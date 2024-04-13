using Application.Core;
using MediatR;
using Persistence.DataTransferObject;
using Persistence.Repositories;

namespace Application.GameRound;

public class PerformOpposingTeamGuess
{
    public class Param
    {
        public Guid GameSessionId { get; set; }

        public Guid UserId { get; set; }

        public bool IsLeft { get; set; }
    }

    public class Command : IRequest<Result<GameRoundOpposingTeamGuessDTO>>
    {
        public Param Param;

        public Command(Param param)
        {
            Param = param;
        }
    }

    public class Handler : IRequestHandler<Command, Result<GameRoundOpposingTeamGuessDTO>>
    {
        private readonly IGameRoundRepository gameRoundRepository;

        public Handler(IGameRoundRepository gameRoundRepository)
        {
            this.gameRoundRepository = gameRoundRepository;
        }

        public async Task<Result<GameRoundOpposingTeamGuessDTO>> Handle(Command request,
            CancellationToken cancellationToken)
        {
            var currentRound = await gameRoundRepository.GetCurrentRound(request.Param.GameSessionId);

            if (currentRound == null)
                return Result<GameRoundOpposingTeamGuessDTO>.Failure(
                    "The current round does not exist, so no guess can be made.");

            var result = await gameRoundRepository.PerformOpposingTeamGuess(
                request.Param.UserId,
                request.Param.GameSessionId,
                currentRound.Id,
                request.Param.IsLeft
            );
            return result == null
                ? Result<GameRoundOpposingTeamGuessDTO>.Failure("Game round guess cannot be performed")
                : Result<GameRoundOpposingTeamGuessDTO>.Success(result);
        }
    }
}