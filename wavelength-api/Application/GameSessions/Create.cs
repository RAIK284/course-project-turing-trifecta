using Domain;
using MediatR;
using Persistence;
using Persistence.Repositories;

namespace Application.GameSessions;

public class Create
{
    public class Params
    {
        public Guid OwnerID { get; set; }
    }
    
    public class Command : IRequest<GameSession>
    {
        public Params Param;

        public Command(Params param)
        {
            this.Param = param;
        }
    }

    public class Handler : IRequestHandler<Command, GameSession>
    {
        private IGameSessionRepository gameSessionRepository;

        public Handler(IGameSessionRepository gameSessionRepository)
        {
            this.gameSessionRepository = gameSessionRepository;
        }
        
        public async Task<GameSession> Handle(Command request, CancellationToken cancellationToken)
        {
            var result = await gameSessionRepository.Create(request.Param.OwnerID);

            return result;
        }
    }
}