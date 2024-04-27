using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.DataTransferObject;

namespace Persistence.Repositories;

public class GameRoundRepository : IGameRoundRepository
{
    private readonly DataContext context;
    private readonly IGameSessionRepository gameSessionRepository;
    private readonly IMapper mapper;
    private readonly Random random;
    private readonly ISpectrumCardRepository spectrumCardRepository;

    public GameRoundRepository(DataContext context, IMapper mapper, ISpectrumCardRepository spectrumCardRepository,
        IGameSessionRepository gameSessionRepository)
    {
        this.context = context;
        this.mapper = mapper;
        this.spectrumCardRepository = spectrumCardRepository;
        this.gameSessionRepository = gameSessionRepository;
        random = new Random();
    }

    /// <inheritdoc />
    public async Task<GameSessionDTO?> StartRound(Guid gameSessionId)
    {
        var previousRoundsForGameSession = await context.GameRounds
            .Where(gr => gr.GameSessionId == gameSessionId)
            .OrderBy(gr => gr.RoundNumber)
            .Include(gr => gr.RoundRoles)
            .ToListAsync();

        var spectrumCards = await spectrumCardRepository.GetAllCards();
        var usedSpectrumCardIds = previousRoundsForGameSession
            .Select(gr => gr.SpectrumCardId)
            .ToList();
        var unusedSpectrumCards = spectrumCards
            .Where(sc => !usedSpectrumCardIds.Contains(sc.Id))
            .ToList();

        var newRound = new GameRound
        {
            RoundNumber = previousRoundsForGameSession.Count,
            GameSessionId = gameSessionId,
            SpectrumCardId = unusedSpectrumCards[random.Next(unusedSpectrumCards.Count)].Id,
            TargetOffset = random.Next(20, 161) // Generate between 20 and 160 inclusive
        };

        if (previousRoundsForGameSession.Any())
        {
            var lastRound = previousRoundsForGameSession.Last();

            newRound.TeamTurn = lastRound.TeamTurn == Team.ONE ? Team.TWO : Team.ONE;
        }
        else
        {
            newRound.TeamTurn = random.Next(1, 3) == 1 ? Team.ONE : Team.TWO;
        }

        newRound.RoundNumber = previousRoundsForGameSession.Count;

        context.GameRounds.Add(newRound);

        if (await context.SaveChangesAsync() > 0)
        {
            var userRoles = await CreateUserRolesForRound(newRound, previousRoundsForGameSession);

            if (userRoles == null || !userRoles.Any()) return null;

            return await gameSessionRepository.Get(gameSessionId);
        }

        return null;
    }

    /// <inheritdoc />
    public async Task<GameRoundDTO?> PsychicGiveClue(Guid gameSessionId, string clue)
    {
        var gameSession = await context.GameSessions
            .Where(gs => gs.Id == gameSessionId)
            .Where(gs => gs.EndTime == null)
            .FirstOrDefaultAsync();

        if (gameSession == null) return null;

        var lastRound = await context.GameRounds
            .Where(gr => gr.GameSessionId == gameSession.Id)
            .Include(gr => gr.RoundRoles)
            .Include(gr => gr.GhostGuesses)
            .Include(gr => gr.SelectorSelection)
            .Include(gr => gr.OpposingGhostGuesses)
            .Include(gr => gr.OpposingSelectorSelection)
            .Include(gr => gr.SpectrumCard)
            .OrderBy(gr => gr.RoundNumber)
            .LastOrDefaultAsync();

        // If the round already has a clue, then the user shouldn't be able to give one
        if (lastRound == null || lastRound.Clue.Length != 0) return null;

        lastRound.Clue = clue;

        await context.SaveChangesAsync();

        return mapper.Map<GameRoundDTO>(lastRound);
    }

    /// <inheritdoc />
    public async Task<GameRoundDTO?> GetCurrentRound(Guid gameSessionId)
    {
        var gameSession = await context.GameSessions
            .Where(gs => gs.Id == gameSessionId)
            .Where(gs => gs.EndTime == null)
            .FirstOrDefaultAsync();

        if (gameSession == null) return null;

        var lastRound = await context.GameRounds
            .Where(gr => gr.GameSessionId == gameSession.Id)
            .Include(gr => gr.RoundRoles)
            .Include(gr => gr.GhostGuesses)
            .Include(gr => gr.SelectorSelection)
            .Include(gr => gr.OpposingGhostGuesses)
            .Include(gr => gr.OpposingSelectorSelection)
            .Include(gr => gr.SpectrumCard)
            .OrderBy(gr => gr.RoundNumber)
            .LastOrDefaultAsync();

        if (lastRound == null) return null;

        return mapper.Map<GameRoundDTO>(lastRound);
    }

    /// <inheritdoc />
    public async Task<GameRoundGhostGuessDTO?> PerformGhostGuess(Guid userId, Guid gameSessionId, Guid gameRoundId,
        int targetOffset)
    {
        var gameSessionMember = await context.GameSessionMembers
            .Where(gsm => gsm.GameSessionId == gameSessionId)
            .Where(gsm => gsm.UserId == userId)
            .FirstOrDefaultAsync();
        var existingGuess = await context.GameRoundGhostGuesses
            .Where(gg => gg.GameSessionId == gameSessionId)
            .Where(gg => gg.GameRoundId == gameRoundId)
            .Where(gg => gg.UserId == userId)
            .FirstOrDefaultAsync();

        if (gameSessionMember == null) return null;

        if (existingGuess == null)
        {
            existingGuess = new GameRoundGhostGuess
            {
                GameSessionId = gameSessionId,
                UserId = userId,
                GameRoundId = gameRoundId,
                Team = gameSessionMember.Team
            };

            context.GameRoundGhostGuesses.Add(existingGuess);
        }

        existingGuess.TargetOffset = targetOffset;

        return await context.SaveChangesAsync() > 0
            ? mapper.Map<GameRoundGhostGuessDTO>(existingGuess)
            : null;
    }

    /// <inheritdoc />
    public async Task<GameRoundSelectorSelectionDTO?> SelectTarget(Guid userId, Guid gameSessionId, Guid gameRoundId,
        int targetOffset)
    {
        var gameSessionMember = await context.GameSessionMembers
            .Where(gsm => gsm.GameSessionId == gameSessionId)
            .Where(gsm => gsm.UserId == userId)
            .FirstOrDefaultAsync();
        var existingSelection = await context.GameRoundSelectorSelections
            .Where(gg => gg.GameSessionId == gameSessionId)
            .Where(gg => gg.GameRoundId == gameRoundId)
            .Where(gg => gg.UserId == userId)
            .FirstOrDefaultAsync();

        if (gameSessionMember == null) return null;

        if (existingSelection == null)
        {
            existingSelection = new GameRoundSelectorSelection
            {
                GameSessionId = gameSessionId,
                UserId = userId,
                GameRoundId = gameRoundId,
                Team = gameSessionMember.Team
            };

            context.GameRoundSelectorSelections.Add(existingSelection);
        }

        existingSelection.TargetOffset = targetOffset;

        return await context.SaveChangesAsync() > 0
            ? mapper.Map<GameRoundSelectorSelectionDTO>(existingSelection)
            : null;
    }

    /// <inheritdoc />
    public async Task<GameRoundOpposingTeamGuessDTO?> PerformOpposingTeamGuess(Guid userId, Guid gameSessionId,
        Guid gameRoundId, bool isLeft)
    {
        var gameSessionMember = await context.GameSessionMembers
            .Where(gsm => gsm.GameSessionId == gameSessionId)
            .Where(gsm => gsm.UserId == userId)
            .FirstOrDefaultAsync();
        var existingGuess = await context.GameRoundOpposingTeamGuesses
            .Where(gg => gg.GameSessionId == gameSessionId)
            .Where(gg => gg.GameRoundId == gameRoundId)
            .Where(gg => gg.UserId == userId)
            .FirstOrDefaultAsync();

        if (gameSessionMember == null) return null;

        if (existingGuess == null)
        {
            existingGuess = new GameRoundOpposingTeamGuess
            {
                GameSessionId = gameSessionId,
                UserId = userId,
                GameRoundId = gameRoundId,
                Team = gameSessionMember.Team
            };

            context.GameRoundOpposingTeamGuesses.Add(existingGuess);
        }

        existingGuess.IsLeft = isLeft;

        return await context.SaveChangesAsync() > 0
            ? mapper.Map<GameRoundOpposingTeamGuessDTO>(existingGuess)
            : null;
    }

    /// <inheritdoc />
    public async Task<GameRoundOpposingTeamSelectionDTO?> PerformOpposingTeamSelection(Guid userId, Guid gameSessionId,
        Guid gameRoundId, bool isLeft)
    {
        var gameSessionMember = await context.GameSessionMembers
            .Where(gsm => gsm.GameSessionId == gameSessionId)
            .Where(gsm => gsm.UserId == userId)
            .FirstOrDefaultAsync();
        var existingSelection = await context.GameRoundOpposingTeamSelections
            .Where(gg => gg.GameSessionId == gameSessionId)
            .Where(gg => gg.GameRoundId == gameRoundId)
            .Where(gg => gg.UserId == userId)
            .FirstOrDefaultAsync();

        if (gameSessionMember == null) return null;

        if (existingSelection == null)
        {
            existingSelection = new GameRoundOpposingTeamSelection
            {
                GameSessionId = gameSessionId,
                UserId = userId,
                GameRoundId = gameRoundId,
                Team = gameSessionMember.Team
            };

            context.GameRoundOpposingTeamSelections.Add(existingSelection);
        }

        existingSelection.IsLeft = isLeft;

        return await context.SaveChangesAsync() > 0
            ? mapper.Map<GameRoundOpposingTeamSelectionDTO>(existingSelection)
            : null;
    }

    /// <inheritdoc />
    public async Task<GameSessionMemberRoundRoleDTO?> GetRoundRole(Guid userId, Guid gameSessionId, Guid gameRoundId)
    {
        return await context.GameSessionMemberRoundRoles
            .Where(rr => rr.GameSessionId == gameSessionId)
            .Where(rr => rr.GameRoundId == gameRoundId)
            .Where(rr => rr.UserId == userId)
            .ProjectTo<GameSessionMemberRoundRoleDTO>(mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
    }

    /// <inheritdoc />
    public async Task<GameRoundDTO?> GetRound(Guid gameSessionId, Guid gameRoundId)
    {
        return await context.GameRounds
            .Where(rr => rr.GameSessionId == gameSessionId)
            .Where(rr => rr.Id == gameRoundId)
            .Include(rr => rr.RoundRoles)
            .Include(rr => rr.GhostGuesses)
            .Include(rr => rr.OpposingGhostGuesses)
            .Include(rr => rr.SelectorSelection)
            .Include(rr => rr.OpposingSelectorSelection)
            .ProjectTo<GameRoundDTO>(mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
    }

    /// <inheritdoc />
    private async Task<List<GameSessionMemberRoundRoleDTO>> CreateUserRolesForRound(GameRound newRound,
        List<GameRound> previousRounds)
    {
        var gameSessionMembers = await context.GameSessionMembers
            .Where(gs => gs.GameSessionId == newRound.GameSessionId)
            .Where(gs => gs.Team != Team.NONE)
            .ToListAsync();

        var gameRoundRoles = new List<GameSessionMemberRoundRole>();

        foreach (var gameSessionMember in gameSessionMembers)
        {
            var roundRole = new GameSessionMemberRoundRole
            {
                GameSessionId = newRound.GameSessionId,
                UserId = gameSessionMember.UserId,
                Team = gameSessionMember.Team,
                GameRoundId = newRound.Id,
                Role = TeamRole.GHOST
            };

            gameRoundRoles.Add(roundRole);
        }

        var roundTeamRoles = gameRoundRoles
            .Where(rr => rr.Team == newRound.TeamTurn)
            .ToList();

        var opposingTeamRoles = gameRoundRoles
            .Where(rr => rr.Team != newRound.TeamTurn)
            .ToList();

        // Make a random user a psychic, prioritizing users that haven't been psychic yet.
        // Then, make a random user a selector, prioritizing users that haven't been selector yet.
        if (roundTeamRoles.Count > 1)
        {
            var currentRoundRolesThatHaventBeenPsychic = roundTeamRoles
                .Where(rr => !HasUserHadRoleBefore(rr.UserId, TeamRole.PSYCHIC, previousRounds))
                .ToList();

            var roundRoleListToUse = currentRoundRolesThatHaventBeenPsychic.Any()
                ? currentRoundRolesThatHaventBeenPsychic
                : roundTeamRoles;

            var psychicIndex = random.Next(roundRoleListToUse.Count);
            var psychicRoundRole = roundRoleListToUse[psychicIndex];
            psychicRoundRole.Role = TeamRole.PSYCHIC;

            // Ensure we prioritize users that haven't been selectors. Also ensure we don't select the
            // user who was made psychic.
            var currentRoundRolesThatHaventBeenSelector = roundTeamRoles
                .Where(rr => rr.UserId != psychicRoundRole.UserId)
                .Where(rr => !HasUserHadRoleBefore(rr.UserId, TeamRole.SELECTOR, previousRounds))
                .ToList();

            roundRoleListToUse = currentRoundRolesThatHaventBeenSelector.Any()
                ? currentRoundRolesThatHaventBeenSelector
                : roundTeamRoles.Where(rr => rr.UserId != psychicRoundRole.UserId).ToList();

            var selectorIndex = random.Next(roundRoleListToUse.Count);
            var selectorRoundRole = roundRoleListToUse[selectorIndex];
            selectorRoundRole.Role = TeamRole.SELECTOR;
        }

        // Make a random user a selector, prioritizing users that have not been selectors
        if (opposingTeamRoles.Any())
        {
            var currentRoundRolesThatHaventBeenSelector = opposingTeamRoles
                .Where(rr => !HasUserHadRoleBefore(rr.UserId, TeamRole.SELECTOR, previousRounds))
                .ToList();

            var roundRoleListToUse = currentRoundRolesThatHaventBeenSelector.Any()
                ? currentRoundRolesThatHaventBeenSelector
                : opposingTeamRoles;

            var selectorIndex = random.Next(roundRoleListToUse.Count);
            roundRoleListToUse[selectorIndex].Role = TeamRole.SELECTOR;
        }

        context.GameSessionMemberRoundRoles.AddRange(gameRoundRoles);

        await context.SaveChangesAsync();

        return mapper.Map<List<GameSessionMemberRoundRoleDTO>>(gameRoundRoles);
    }

    private bool HasUserHadRoleBefore(Guid userId, TeamRole role, List<GameRound> previousRounds)
    {
        return previousRounds
            .Select(pr => pr.RoundRoles)
            .Any(roundRoles =>
                roundRoles
                    .Where(rr => rr.UserId == userId)
                    .Any(rr => rr.Role == role)
            );
    }
}