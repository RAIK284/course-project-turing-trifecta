using Application.Core;
using MediatR;
using Persistence.DataTransferObject;
using Persistence.Repositories;

namespace Application.GameRound;

public class PerformOpposingTeamSelection
{
    public class Param
    {
        public Guid GameSessionID { get; set; }

        public Guid UserID { get; set; }

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
        private readonly IGameRoundRepository gameRoundRepository;

        public Handler(IGameRoundRepository gameRoundRepository)
        {
            this.gameRoundRepository = gameRoundRepository;
        }

        public async Task<Result<GameRoundOpposingTeamSelectionDTO>> Handle(Command request,
            CancellationToken cancellationToken)
        {
            var currentRound = await gameRoundRepository.GetCurrentRound(request.Param.GameSessionID);

            if (currentRound == null)
                return Result<GameRoundOpposingTeamSelectionDTO>.Failure(
                    "The current round does not exist, so no guess can be made.");

            var result = await gameRoundRepository.PerformOpposingTeamSelection(
                request.Param.UserID,
                request.Param.GameSessionID,
                currentRound.ID,
                request.Param.IsLeft
            );

            return result == null
                ? Result<GameRoundOpposingTeamSelectionDTO>.Failure("Game round selection cannot be performed")
                : Result<GameRoundOpposingTeamSelectionDTO>.Success(result);
        }
    }
}