using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.DataTransferObject;

namespace Persistence.Repositories;

public class GameSessionRepository : IGameSessionRepository
{
    private readonly DataContext context;
    private readonly IMapper mapper;

    public GameSessionRepository(DataContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<GameSessionDTO> Create(Guid ownerID)
    {
        var newGameSession = new GameSession
        {
            OwnerID = ownerID,
            JoinCode = GenerateRandomJoinCode()
        };

        context.GameSessions.Add(newGameSession);

        await context.SaveChangesAsync();

        // Have the user join the game
        await Join(newGameSession.ID, ownerID);

        return mapper.Map<GameSessionDTO>(newGameSession);
    }

    public async Task<GameSessionDTO?> Get(Guid gameSessionID)
    {
        return await context.GameSessions
            .Where(gs => gs.ID == gameSessionID)
            .Include(gs => gs.Members)
            .ProjectTo<GameSessionDTO>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }

    public async Task<GameSessionMemberDTO?> Join(Guid gameSessionID, Guid userID)
    {
        var gameSessionMember = new GameSessionMember
        {
            UserID = userID,
            GameSessionID = gameSessionID,
            Team = 0
        };

        context.GameSessionMembers.Add(gameSessionMember);

        await context.SaveChangesAsync();

        return mapper.Map<GameSessionMemberDTO>(gameSessionMember);
    }

    public async Task<bool> Leave(Guid gameSessionID, Guid userID)
    {
        var gameSession = await context.GameSessions
            .Where(gs => gs.ID == gameSessionID)
            .FirstOrDefaultAsync();
        var gameSessionMember = await context.GameSessionMembers
            .Where(gsm => gsm.GameSessionID == gameSessionID)
            .Where(gsm => gsm.UserID == userID)
            .FirstOrDefaultAsync();

        // If the game sessions owner is the user, delete the game session
        if (gameSession != null && gameSession.OwnerID == userID) context.GameSessions.Remove(gameSession);

        if (gameSessionMember != null) context.GameSessionMembers.Remove(gameSessionMember);

        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> End(Guid gameSessionID)
    {
        var gameSession = await context.GameSessions
            .Where(gs => gs.ID == gameSessionID)
            .FirstOrDefaultAsync();

        gameSession.EndTime = DateTime.Now;

        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Start(Guid gameSessionID)
    {
        var gameSession = await context.GameSessions
            .Where(gs => gs.ID == gameSessionID)
            .FirstOrDefaultAsync();

        gameSession.StartTime = DateTime.Now;

        return await context.SaveChangesAsync() > 0;
    }

    private string GenerateRandomJoinCode()
    {
        var random = new Random();
        var joinCode = "";

        while (joinCode.Length == 0)
        {
            /// generate a value between 000,000 and 999,999 inclusive
            var randomCode = random.Next(1000000);
            var formattedCode = randomCode.ToString("D6");

            var isCodeInUse = context.GameSessions
                .Where(gs => gs.EndTime == null)
                .Any(gs => gs.JoinCode == formattedCode);

            if (!isCodeInUse) joinCode = formattedCode;
        }

        return joinCode;
    }
}