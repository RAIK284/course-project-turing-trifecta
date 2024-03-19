using Application.Core;
using MediatR;
using Persistence.DataTransferObject;
using Persistence.Repositories;

namespace Application.GameRound;

public class PerformGhostGuess
{
    public class Param
    {
        public Guid GameSessionID { get; set; }

        public Guid UserID { get; set; }

        public int TargetOffset { get; set; }
    }


    public class Command : IRequest<Result<GameRoundGhostGuessDTO>>
    {
        public Param Param;

        public Command(Param param)
        {
            Param = param;
        }
    }

    public class Handler : IRequestHandler<Command, Result<GameRoundGhostGuessDTO>>
    {
        private readonly IGameRoundRepository gameRoundRepository;

        public Handler(IGameRoundRepository gameRoundRepository)
        {
            this.gameRoundRepository = gameRoundRepository;
        }

        public async Task<Result<GameRoundGhostGuessDTO>> Handle(Command request, CancellationToken cancellationToken)
        {
            var currentRound = await gameRoundRepository.GetCurrentRound(request.Param.GameSessionID);

            if (currentRound == null)
                return Result<GameRoundGhostGuessDTO>.Failure(
                    "The current round does not exist, so no guess can be made.");

            var result = await gameRoundRepository.PerformGhostGuess(
                request.Param.UserID,
                request.Param.GameSessionID,
                currentRound.ID,
                request.Param.TargetOffset
            );

            return result == null
                ? Result<GameRoundGhostGuessDTO>.Failure("Game round guess cannot be performed")
                : Result<GameRoundGhostGuessDTO>.Success(result);
        }
    }
}