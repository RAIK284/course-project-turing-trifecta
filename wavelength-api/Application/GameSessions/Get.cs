using Application.Core;
using MediatR;
using Persistence.DataTransferObject;
using Persistence.Repositories;

namespace Application.GameSessions;

public class Get
{
    public class Params
    {
        public Guid ID { get; init; }
    }

    public class Query : IRequest<Result<GameSessionDTO>>
    {
        public readonly Params Param;

        public Query(Params param)
        {
            Param = param;
        }
    }

    public class Handler : IRequestHandler<Query, Result<GameSessionDTO>>
    {
        private readonly IGameSessionRepository gameSessionRepository;

        public Handler(IGameSessionRepository gameSessionRepository)
        {
            this.gameSessionRepository = gameSessionRepository;
        }

        public async Task<Result<GameSessionDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await gameSessionRepository.Get(request.Param.ID);

            return result == null
                ? Result<GameSessionDTO>.Failure("The game session you requested was not found.")
                : Result<GameSessionDTO>.Success(result);
        }
    }
}