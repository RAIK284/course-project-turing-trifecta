using Application.Core;
using Application.HubServices;
using Domain;
using MediatR;
using Persistence.DataTransferObject;
using Persistence.Repositories;

namespace Application.GameSessions;

public class Start
{
    public class Params
    {
        public Guid GameSessionId { get; set; }
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
        private readonly IGameRoundRepository gameRoundRepository;
        private readonly IGameSessionMemberRepository gameSessionMemberRepository;
        private readonly IGameSessionRepository gameSessionRepository;
        private readonly IGameRoundHubService roundHubService;

        public Handler(IGameSessionRepository gameSessionRepository,
            IGameRoundRepository gameRoundRepository,
            IGameRoundHubService roundHubService,
            IGameSessionMemberRepository gameSessionMemberRepository)
        {
            this.gameSessionRepository = gameSessionRepository;
            this.gameRoundRepository = gameRoundRepository;
            this.roundHubService = roundHubService;
            this.gameSessionMemberRepository = gameSessionMemberRepository;
        }

        public async Task<Result<GameSessionDTO>> Handle(Command request, CancellationToken cancellationToken)
        {
            var members = await gameSessionMemberRepository.AssignTeamlessPlayersToTeam(request.Param.GameSessionId);
            var membersOnTeamOne = members.Count(m => m.Team == Team.ONE);
            var membersOnTeamTwo = members.Count(m => m.Team == Team.TWO);

            if (membersOnTeamOne < 2 || membersOnTeamTwo < 2)
                return Result<GameSessionDTO>.Failure("Each team must have two players for the game to start.");

            var isGameStarted = await gameSessionRepository.Start(request.Param.GameSessionId);

            if (!isGameStarted) return Result<GameSessionDTO>.Failure("Failed to start game session.");

            var gameSession = await gameRoundRepository.StartRound(request.Param.GameSessionId);

            if (gameSession == null) return Result<GameSessionDTO>.Failure("Unable to start game round.");

            await roundHubService.NotifyRoundStart(request.Param.GameSessionId, gameSession);

            return Result<GameSessionDTO>.Success(gameSession);
        }
    }
}