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
        public Guid GameSessionID { get; set; }

        public Guid UserID { get; set; }
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
            var member = await gameSessionRepository.Join(request.Param.GameSessionID, request.Param.UserID);

            if (member == null) return Result<GameSessionDTO>.Failure("You cannot join this game session.");

            var gameSession = await gameSessionRepository.Get(request.Param.GameSessionID);

            await sessionHubService.NotifyUserJoined(request.Param.GameSessionID, member);

            return gameSession == null
                ? Result<GameSessionDTO>.Failure("Something went wrong when joining the game.")
                : Result<GameSessionDTO>.Success(gameSession);
        }
    }
}