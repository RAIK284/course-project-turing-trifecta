using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using MediatR;
using Persistence.DataTransferObject;
using Persistence.Repositories;


namespace Application.GameRound
{
    public class SelectTarget
    {
        

    public class Command : IRequest<Result<GameRoundSelectorSelectionDTO>>
    {
        public GameRoundSelectorSelectionDTO Param;

        public Command(GameRoundSelectorSelectionDTO param)
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

        public async Task<Result<GameRoundSelectorSelectionDTO>> Handle(Command request, CancellationToken cancellationToken)
        {
           var result = await gameRoundRepository.SelectTarget(
            request.Param.UserID,
            request.Param.GameSessionID,
            request.Param.GameRoundID,
            request.Param.TargetOffset
           );
           return result == null 
           ? Result<GameRoundSelectorSelectionDTO>.Failure("Game round target cannot be selected") 
           : Result<GameRoundSelectorSelectionDTO>.Success(result);
        }
    }
}

}