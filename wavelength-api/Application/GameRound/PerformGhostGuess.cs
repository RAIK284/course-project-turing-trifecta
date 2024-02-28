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
    public class PerformGhostGuess
    {
       

    public class Command : IRequest<Result<GameRoundGhostGuessDTO>>
    {
        public GameRoundGhostGuessDTO Param;

        public Command(GameRoundGhostGuessDTO param)
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
           var result = await gameRoundRepository.PerformGhostGuess(
            request.Param.UserID,
            request.Param.GameSessionID,
            request.Param.GameRoundID,
            request.Param.TargetOffset
           );
           return result == null 
           ? Result<GameRoundGhostGuessDTO>.Failure("Game round guess cannot be performed") 
           : Result<GameRoundGhostGuessDTO>.Success(result);
        }
    }
    }
}