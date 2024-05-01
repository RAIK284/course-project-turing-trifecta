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

    public async Task<GameSessionDTO?> Create(Guid ownerId)
    {
        // If a user is a part of an active game session, disallow them from creating a new one.
        if (await GetActiveSession(ownerId) != null) return null;

        var newGameSession = new GameSession
        {
            OwnerId = ownerId,
            JoinCode = GenerateRandomJoinCode()
        };

        context.GameSessions.Add(newGameSession);

        await context.SaveChangesAsync();

        // Have the user join the game
        await Join(newGameSession.Id, ownerId);

        return await Get(newGameSession.Id);
    }

    public async Task<GameSessionDTO?> Get(Guid gameSessionId)
    {
        var gameSession = await context.GameSessions
            .Where(gs => gs.Id == gameSessionId)
            .Include(gs => gs.Members)
            .ThenInclude(gsm => gsm.User)
            .Include(gsm => gsm.Rounds)
            .ThenInclude(r => r.GhostGuesses)
            .Include(gsm => gsm.Rounds)
            .ThenInclude(r => r.SelectorSelection)
            .Include(gsm => gsm.Rounds)
            .ThenInclude(r => r.RoundRoles)
            .Include(gsm => gsm.Rounds)
            .ThenInclude(r => r.OpposingGhostGuesses)
            .Include(gsm => gsm.Rounds)
            .ThenInclude(r => r.OpposingSelectorSelection)
            .Include(gs => gs.GameSessionResult)
            .ProjectTo<GameSessionDTO>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        if (gameSession == null) return null;

        gameSession.Rounds = gameSession.Rounds.OrderBy(r => r.RoundNumber).ToList();

        return gameSession;
    }

    public async Task<GameSessionMemberDTO?> Join(Guid gameSessionId, Guid userId)
    {
        var gameSessionMember = new GameSessionMember
        {
            UserId = userId,
            GameSessionId = gameSessionId,
            Team = 0
        };

        context.GameSessionMembers.Add(gameSessionMember);

        await context.SaveChangesAsync();

        return await context.GameSessionMembers
            .Where(gsm => gsm.UserId == userId)
            .Include(gsm => gsm.User)
            .Include(gsm => gsm.GameSession)
            .ProjectTo<GameSessionMemberDTO>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }

    public async Task<GameSessionDTO?> GetByJoinCode(string joinCode)
    {
        return await context.GameSessions
            .Where(gs => gs.JoinCode == joinCode)
            .Include(gs => gs.Members)
            .ThenInclude(gsm => gsm.User)
            .ProjectTo<GameSessionDTO>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> Leave(Guid gameSessionId, Guid userId)
    {
        var gameSession = await context.GameSessions
            .Where(gs => gs.Id == gameSessionId)
            .FirstOrDefaultAsync();
        var gameSessionMember = await context.GameSessionMembers
            .Where(gsm => gsm.GameSessionId == gameSessionId)
            .Where(gsm => gsm.UserId == userId)
            .FirstOrDefaultAsync();

        // If the game sessions owner is the user, delete the game session
        if (gameSession != null && gameSession.OwnerId == userId) context.GameSessions.Remove(gameSession);

        if (gameSessionMember != null) context.GameSessionMembers.Remove(gameSessionMember);

        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> End(Guid gameSessionId)
    {
        var gameSession = await context.GameSessions
            .Where(gs => gs.Id == gameSessionId)
            .FirstOrDefaultAsync();

        if (gameSession == null) return false;

        gameSession.EndTime = DateTime.Now;

        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Start(Guid gameSessionId)
    {
        var gameSession = await context.GameSessions
            .Where(gs => gs.Id == gameSessionId)
            .FirstOrDefaultAsync();

        if (gameSession == null) return false;

        if (gameSession.StartTime == null)
        {
            gameSession.StartTime = DateTime.Now;
            await context.SaveChangesAsync();
        }

        return true;
    }

    public async Task<GameSessionDTO?> GetActiveSession(Guid userId)
    {
        var gameSessionIdsForUser = await context.GameSessionMembers
            .Where(gsm => gsm.UserId == userId)
            .Select(gsm => gsm.GameSessionId)
            .ToListAsync();

        if (!gameSessionIdsForUser.Any()) return null;

        var gameSessionId = await context.GameSessions
            .Where(gs => gs.EndTime == null)
            .Where(gs => gameSessionIdsForUser.Contains(gs.Id))
            .FirstOrDefaultAsync();


        return gameSessionId == null ? null : await Get(gameSessionId.Id);
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

    public static GameSessionDTO? AdjustTargetOffsets(Guid userId, GameSessionDTO? gameSession)
    {
        if (gameSession == null || gameSession.GameRound < 0 || !gameSession.Rounds.Any()) return gameSession;

        var gameRound = gameSession.Rounds[gameSession.GameRound];
        var roundRole = gameRound.RoundRoles
            .SingleOrDefault(rr => rr.UserId == userId);

        if (roundRole != null && roundRole.Role != TeamRole.PSYCHIC)
            gameRound.TargetOffset = -1;

        return gameSession;
    }

    public async Task<GameSessionDTO?> UpdateGameScoring(Guid gameSessionId)
    {
        var gameSession = await Get(gameSessionId);

        if (gameSession == null) return null;

        var gameSessionResult = CalculateScoring(gameSession);

        var existingGameSessionResult = await context.GameSessionResults
            .Where(gs => gs.GameSessionId == gameSession.Id)
            .FirstOrDefaultAsync();

        if (existingGameSessionResult == null)
        {
            context.GameSessionResults.Add(gameSessionResult);
        }
        else
        {
            existingGameSessionResult.WinningTeam = gameSessionResult.WinningTeam;
            existingGameSessionResult.WinningScore = gameSessionResult.WinningScore;
            existingGameSessionResult.LosingTeam = gameSessionResult.LosingTeam;
            existingGameSessionResult.LosingScore = gameSessionResult.LosingScore;
        }

        await context.SaveChangesAsync();

        return await Get(gameSessionId);
    }

    private GameSessionResult CalculateScoring(GameSessionDTO gameSession)
    {
        var teamOneScore = 0;
        var teamTwoScore = 0;
        foreach (var round in gameSession.Rounds)
        {
            var teamTurn = round.TeamTurn;
            var targetOffset = round.TargetOffset;
            var selection = round.SelectorSelection.TargetOffset;
            var isLeftSelection = round.OpposingTeamSelection.IsLeft;

            var teamGuessDistance = Math.Abs(targetOffset - selection);
            var wasSelectionLeft = targetOffset - selection > 0;
            var teamTurnScore = 0;
            var opposingTeamTurnScore = 0;

            if (teamGuessDistance <= 4)
            {
                teamTurnScore = 4;
            } else if (teamGuessDistance <= 12)
            {
                teamTurnScore = 3;
            } else if (teamGuessDistance <= 20)
            {
                teamTurnScore = 2;
            }

            if (teamGuessDistance > 4)
            {
                if (wasSelectionLeft && isLeftSelection)
                {
                    opposingTeamTurnScore = 1;
                } else if (!wasSelectionLeft && !isLeftSelection)
                {
                    opposingTeamTurnScore = 1;
                }
            }

            if (teamTurn == Team.ONE)
            {
                teamOneScore += teamTurnScore;
                teamTwoScore += opposingTeamTurnScore;
            }
            else
            {
                teamOneScore += opposingTeamTurnScore;
                teamTwoScore += teamTurnScore;
            }
        }

        var gameSessionResult = new GameSessionResult
        {
            GameSessionId = gameSession.Id,
            LosingTeam = teamOneScore < teamTwoScore ? Team.ONE : Team.TWO,
            LosingScore = Math.Min(teamOneScore, teamTwoScore),
            WinningTeam = teamOneScore > teamTwoScore ? Team.ONE : Team.TWO,
            WinningScore = Math.Max(teamOneScore, teamTwoScore),
        };

        return gameSessionResult;
    }
}