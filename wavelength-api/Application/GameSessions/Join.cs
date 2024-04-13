using Application.Core;
using Application.HubServices;
using MediatR;
using Persistence.DataTransferObject;
using Persistence.Repositories;

namespace Application.GameSessions;

public class Join
{
    public class Params
    {
        public string JoinCode { get; set; }

        public Guid UserId { get; set; }
    }

    public class Command : IRequest<Result<GameSessionDTO>>
    {
        public Params Param;

        public Command(Params param)
        {
            Param = param;
        }
    }

    public class Handler : IRequestHandler<Command, Result<GameSessionDTO>>
    {
        private readonly IGameSessionRepository gameSessionRepository;
        private readonly IGameSessionHubService sessionHubService;

        public Handler(IGameSessionRepository gameSessionRepository,
            IGameSessionHubService sessionHubService)
        {
            this.gameSessionRepository = gameSessionRepository;
            this.sessionHubService = sessionHubService;
        }

        public async Task<Result<GameSessionDTO>> Handle(Command request, CancellationToken cancellationToken)
        {
            var gameSession = await gameSessionRepository.GetByJoinCode(request.Param.JoinCode);

            if (gameSession == null) return Result<GameSessionDTO>.Failure("Invalid join code.");

            var member = await gameSessionRepository.Join(gameSession.Id, request.Param.UserId);

            if (member == null) return Result<GameSessionDTO>.Failure("You cannot join this game session.");

            await sessionHubService.NotifyUserJoined(gameSession.Id, member);

            if (gameSession.Members != null)
                gameSession.Members.Add(member);

            return Result<GameSessionDTO>.Success(gameSession);
        }
    }
}