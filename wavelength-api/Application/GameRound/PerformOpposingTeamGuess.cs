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
    public class PerformOpposingTeamGuess
    {
        
        public class Command : IRequest<Result<GameRoundOpposingTeamGuessDTO>>
        {
            public GameRoundOpposingTeamGuessDTO Param;

            public Command(GameRoundOpposingTeamGuessDTO param)
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

            public async Task<Result<GameRoundOpposingTeamGuessDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
               var result = await gameRoundRepository.PerformOpposingTeamGuess(
                request.Param.UserID,
                request.Param.GameSessionID,
                request.Param.GameRoundID,
                request.Param.IsLeft
               );
               return result == null 
               ? Result<GameRoundOpposingTeamGuessDTO>.Failure("Game round guess cannot be performed") 
               : Result<GameRoundOpposingTeamGuessDTO>.Success(result);
            }
    }
}
}