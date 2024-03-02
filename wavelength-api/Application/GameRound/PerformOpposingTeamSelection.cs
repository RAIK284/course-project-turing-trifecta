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
    public class PerformOpposingTeamSelection
    {
        public class Command : IRequest<Result<GameRoundOpposingTeamSelectionDTO>>
        {
            public GameRoundOpposingTeamSelectionDTO Param;

            public Command(GameRoundOpposingTeamSelectionDTO param)
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

            public async Task<Result<GameRoundOpposingTeamSelectionDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var result = await gameRoundRepository.PerformOpposingTeamSelection(
                    request.Param.UserID,
                    request.Param.GameSessionID,
                    request.Param.GameRoundID,
                    request.Param.IsLeft
                );
                return result == null
                    ? Result<GameRoundOpposingTeamSelectionDTO>.Failure("Game round selection cannot be performed")
                    : Result<GameRoundOpposingTeamSelectionDTO>.Success(result);
            }
    }
}
}