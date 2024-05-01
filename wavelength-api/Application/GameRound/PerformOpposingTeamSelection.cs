using Application.Core;
using Application.HubServices;
using MediatR;
using Persistence.DataTransferObject;
using Persistence.Repositories;

namespace Application.GameRound;

public class PerformOpposingTeamSelection
{
    public class Param
    {
        public Guid GameSessionId { get; set; }

        public Guid UserId { get; set; }

        public bool IsLeft { get; set; }
    }

    public class Command : IRequest<Result<GameSessionDTO>>
    {
        public Param Param;

        public Command(Param param)
        {
            Param = param;
        }
    }

    public class Handler : IRequestHandler<Command, Result<GameSessionDTO>>
    {
        private readonly IGameRoundHubService gameRoundHubService;
        private readonly IGameRoundRepository gameRoundRepository;
        private readonly IGameSessionRepository gameSessionRepository;

        public Handler(IGameRoundRepository gameRoundRepository,
            IGameRoundHubService gameRoundHubService,
            IGameSessionRepository gameSessionRepository)
        {
            this.gameRoundRepository = gameRoundRepository;
            this.gameRoundHubService = gameRoundHubService;
            this.gameSessionRepository = gameSessionRepository;
        }

        public async Task<Result<GameSessionDTO>> Handle(Command request,
            CancellationToken cancellationToken)
        {
            var currentRound = await gameRoundRepository.GetCurrentRound(request.Param.GameSessionId);

            if (currentRound == null)
                return Result<GameSessionDTO>.Failure(
                    "The current round does not exist, so no guess can be made.");

            var result = await gameRoundRepository.PerformOpposingTeamSelection(
                request.Param.UserId,
                request.Param.GameSessionId,
                currentRound.Id,
                request.Param.IsLeft
            );

            if (result == null)
                return Result<GameSessionDTO>.Failure("Game round selection cannot be performed");

            var updatedGameSession =
                await gameSessionRepository.UpdateGameScoring(request.Param.GameSessionId);

            if (updatedGameSession == null)
            {
                return Result<GameSessionDTO>.Failure(
                    "Something went wrong when updating the game score.");
            }

            await gameRoundHubService.NotifyRoundEnd(result.GameSessionId, updatedGameSession);

            return Result<GameSessionDTO>.Success(updatedGameSession);
        }
    }
}