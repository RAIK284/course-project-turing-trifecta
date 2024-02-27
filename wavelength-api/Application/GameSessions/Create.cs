using Application.Core;
using MediatR;
using Persistence.DataTransferObject;
using Persistence.Repositories;

namespace Application.GameSessions;

public class Create
{
    public class Params
    {
        public Guid OwnerID { get; set; }
    }

    public class Command : IRequest<Result<GameSessionDTO>>
    {
        public Params Param;

        public Command(Params param)
        {
            Param = param;
        }
    }

    public class Handler : IRequestHandler<Command, Result<GameSessionDTO>>
    {
        private readonly IGameSessionRepository gameSessionRepository;

        public Handler(IGameSessionRepository gameSessionRepository)
        {
            this.gameSessionRepository = gameSessionRepository;
        }

        public async Task<Result<GameSessionDTO>> Handle(Command request, CancellationToken cancellationToken)
        {
            var result = await gameSessionRepository.Create(request.Param.OwnerID);

            return result == null
                ? Result<GameSessionDTO>.Failure("Unable to create game session.")
                : Result<GameSessionDTO>.Success(result);
        }
    }
}