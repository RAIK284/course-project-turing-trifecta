using Application.Core;
using Application.HubServices;
using Domain;
using MediatR;
using Persistence.DataTransferObject;
using Persistence.Repositories;

namespace Application.GameSessions;

public class SwitchTeams
{
    public class Params
    {
        public Guid GameSessionId { get; set; }

        public Guid UserId { get; set; }

        public Team Team { get; set; }
    }

    public class Command : IRequest<Result<GameSessionMemberDTO>>
    {
        public Params Param;

        public Command(Params param)
        {
            Param = param;
        }
    }

    public class Handler : IRequestHandler<Command, Result<GameSessionMemberDTO>>
    {
        private readonly IGameSessionHubService gameSessionHubService;
        private readonly IGameSessionMemberRepository gameSessionMemberRepository;
        private readonly IGameSessionRepository gameSessionRepository;

        public Handler(
            IGameSessionMemberRepository gameSessionMemberRepository,
            IGameSessionHubService gameSessionHubService,
            IGameSessionRepository gameSessionRepository)
        {
            this.gameSessionMemberRepository = gameSessionMemberRepository;
            this.gameSessionHubService = gameSessionHubService;
            this.gameSessionRepository = gameSessionRepository;
        }

        public async Task<Result<GameSessionMemberDTO>> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request.Param.Team != Team.ONE && request.Param.Team != Team.TWO)
                return Result<GameSessionMemberDTO>.Failure("Either team one or team two must be chosen.");

            var gameSession = await gameSessionRepository.Get(request.Param.GameSessionId);

            if (gameSession == null) return Result<GameSessionMemberDTO>.Failure("The game session does not exist.");

            if (gameSession.StartTime != null)
                return Result<GameSessionMemberDTO>.Failure("You cannot switch teams after the game has started.");

            var members = await gameSessionMemberRepository.GetAll(request.Param.GameSessionId);
            var membersOnTeamOne = members.Count(m => m.Team == Team.ONE);
            var membersOnTeamTwo = members.Count(m => m.Team == Team.TWO);

            var isAlreadyOnTeam = members
                .Where(gm => gm.UserId == request.Param.UserId)
                .Any(gm => gm.Team == request.Param.Team);

            if (isAlreadyOnTeam) return Result<GameSessionMemberDTO>.Failure("You are already on this team.");

            // If the difference between team one's players and team two's players is >= 1, the team is full
            if (request.Param.Team == Team.ONE && membersOnTeamOne - membersOnTeamTwo > 0)
                return Result<GameSessionMemberDTO>.Failure("Team one is too full to join.");

            if (request.Param.Team == Team.TWO && membersOnTeamTwo - membersOnTeamTwo > 0)
                return Result<GameSessionMemberDTO>.Failure("Team two is too full to join.");

            var result = await gameSessionMemberRepository.JoinTeam(request.Param.UserId, request.Param.GameSessionId,
                request.Param.Team);

            if (result == null)
                return Result<GameSessionMemberDTO>.Failure("Something went wrong when switching teams.");

            await gameSessionHubService.NotifyUserJoinedTeam(request.Param.GameSessionId, result);

            return Result<GameSessionMemberDTO>.Success(result);
        }
    }
}