using Application.Core;
using Application.HubServices;
using MediatR;
using Persistence.DataTransferObject;
using Persistence.Repositories;

namespace Application.GameRound;

public class GiveClue
{
    public class Params
    {
        public Guid GameSessionID { get; set; }

        public string Clue { get; set; }
    }

    public class Command : IRequest<Result<GameRoundDTO>>
    {
        public Params Param;

        public Command(Params param)
        {
            Param = param;
        }
    }

    public class Handler : IRequestHandler<Command, Result<GameRoundDTO>>
    {
        private readonly IGameRoundRepository gameRoundRepository;
        private readonly IGameRoundHubService roundHubService;

        public Handler(
            IGameRoundRepository gameRoundRepository,
            IGameRoundHubService roundHubService)
        {
            this.gameRoundRepository = gameRoundRepository;
            this.roundHubService = roundHubService;
        }

        public async Task<Result<GameRoundDTO>> Handle(Command request, CancellationToken cancellationToken)
        {
            var result = await gameRoundRepository.PsychicGiveClue(request.Param.GameSessionID, request.Param.Clue);

            if (result == null) return Result<GameRoundDTO>.Failure("Could not make a clue for the current round.");

            await roundHubService.NotifyPsychicClue(request.Param.GameSessionID, result);

            return Result<GameRoundDTO>.Success(result);
        }
    }
}