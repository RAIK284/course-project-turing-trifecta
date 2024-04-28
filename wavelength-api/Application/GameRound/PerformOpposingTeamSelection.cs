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

    public class Command : IRequest<Result<GameRoundOpposingTeamSelectionDTO>>
    {
        public Param Param;

        public Command(Param param)
        {
            Param = param;
        }
    }

    public class Handler : IRequestHandler<Command, Result<GameRoundOpposingTeamSelectionDTO>>
    {
        private readonly IGameRoundHubService gameRoundHubService;
        private readonly IGameRoundRepository gameRoundRepository;

        public Handler(IGameRoundRepository gameRoundRepository,
            IGameRoundHubService gameRoundHubService)
        {
            this.gameRoundRepository = gameRoundRepository;
            this.gameRoundHubService = gameRoundHubService;
        }

        public async Task<Result<GameRoundOpposingTeamSelectionDTO>> Handle(Command request,
            CancellationToken cancellationToken)
        {
            var currentRound = await gameRoundRepository.GetCurrentRound(request.Param.GameSessionId);

            if (currentRound == null)
                return Result<GameRoundOpposingTeamSelectionDTO>.Failure(
                    "The current round does not exist, so no guess can be made.");

            var result = await gameRoundRepository.PerformOpposingTeamSelection(
                request.Param.UserId,
                request.Param.GameSessionId,
                currentRound.Id,
                request.Param.IsLeft
            );

            if (result == null)
                return Result<GameRoundOpposingTeamSelectionDTO>.Failure("Game round selection cannot be performed");

            await gameRoundHubService.NotifyOpposingTeamSelectorSelect(result.GameSessionId, result);

            return Result<GameRoundOpposingTeamSelectionDTO>.Success(result);
        }
    }
}