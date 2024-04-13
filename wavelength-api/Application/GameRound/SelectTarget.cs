using Application.Core;
using MediatR;
using Persistence.DataTransferObject;
using Persistence.Repositories;

namespace Application.GameRound;

public class SelectTarget
{
    public class Param
    {
        public Guid GameSessionId { get; set; }

        public Guid UserId { get; set; }

        public int TargetOffset { get; set; }
    }

    public class Command : IRequest<Result<GameRoundSelectorSelectionDTO>>
    {
        public Param Param;

        public Command(Param param)
        {
            Param = param;
        }
    }

    public class Handler : IRequestHandler<Command, Result<GameRoundSelectorSelectionDTO>>
    {
        private readonly IGameRoundRepository gameRoundRepository;

        public Handler(IGameRoundRepository gameRoundRepository)
        {
            this.gameRoundRepository = gameRoundRepository;
        }

        public async Task<Result<GameRoundSelectorSelectionDTO>> Handle(Command request,
            CancellationToken cancellationToken)
        {
            var currentRound = await gameRoundRepository.GetCurrentRound(request.Param.GameSessionId);

            if (currentRound == null)
                return Result<GameRoundSelectorSelectionDTO>.Failure(
                    "The current round does not exist, so no selection can be made.");

            var result = await gameRoundRepository.SelectTarget(
                request.Param.UserId,
                request.Param.GameSessionId,
                currentRound.Id,
                request.Param.TargetOffset
            );

            return result == null
                ? Result<GameRoundSelectorSelectionDTO>.Failure("Game round target cannot be selected")
                : Result<GameRoundSelectorSelectionDTO>.Success(result);
        }
    }
}