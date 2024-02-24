using Domain;
using MediatR;
using Persistence;

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
        private DataContext context;

        public Handler(DataContext context)
        {
            this.context = context;
        }
        
        public async Task<GameSession> Handle(Command request, CancellationToken cancellationToken)
        {
            var newGameSession = new GameSession
            {
                JoinCode = GenerateRandomJoinCode(context),
                StartTime = DateTime.Now,
                OwnerID = request.Param.OwnerID,
            };

            var added = context.GameSessions.Add(newGameSession);

            await context.SaveChangesAsync();

            return added.Entity;
        }

        private String GenerateRandomJoinCode(DataContext context)
        {
            Random random = new Random();
            String joinCode = "";

            while (joinCode.Length == 0)
            {
                /// generate a value between 000,000 and 999,999 inclusive
                int randomCode = random.Next(1000000);
                String formattedCode = randomCode.ToString("D6");

                var isCodeInUse = context.GameSessions
                    .Where(gs => gs.EndTime == null)
                    .Any(gs => gs.JoinCode == formattedCode);

                if (!isCodeInUse)
                {
                    joinCode = formattedCode;
                }
            }

            return joinCode;
        }
    }
}