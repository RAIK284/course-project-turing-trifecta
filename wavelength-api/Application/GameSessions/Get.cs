using Domain;
using MediatR;
using Persistence;

namespace Application.GameSessions;

public class Get
{
    public class Params
    {
        public Guid ID { get; init; }
    }
    public class Query : IRequest<GameSession>
    {
        public readonly Params param;
        
        public Query(Params param)
        {
            this.param = param;
        }
    }

    public class Handler : IRequestHandler<Query, GameSession>
    {

        private readonly DataContext context;
        
        public Handler(DataContext context)
        {
            this.context = context;
        }
        
        public async Task<GameSession> Handle(Query request, CancellationToken cancellationToken)
        {
            return await context.GameSessions.FindAsync(request.param.ID);
        }
    }
}