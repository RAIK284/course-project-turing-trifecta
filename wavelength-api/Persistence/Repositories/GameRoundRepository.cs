using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.DataTransferObject;

namespace Persistence.Repositories;

public class GameRoundRepository : IGameRoundRepository
{
    private readonly DataContext context;
    private readonly IMapper mapper;
    private readonly Random random;
    private readonly ISpectrumCardRepository spectrumCardRepository;

    public GameRoundRepository(DataContext context, IMapper mapper, ISpectrumCardRepository spectrumCardRepository)
    {
        this.context = context;
        this.mapper = mapper;
        this.spectrumCardRepository = spectrumCardRepository;
        random = new Random();
    }

    /// <inheritdoc />
    public async Task<GameRoundDTO?> StartRound(Guid gameSessionID)
    {
        var previousRoundsForGameSession = await context.GameRounds
            .Where(gr => gr.GameSessionID == gameSessionID)
            .OrderBy(gr => gr.RoundNumber)
            .Include(gr => gr.RoundRoles)
            .ToListAsync();

        var spectrumCards = await spectrumCardRepository.GetAllCards();
        var usedSpectrumCardIDs = previousRoundsForGameSession
            .Select(gr => gr.SpectrumCardID)
            .ToList();
        var unusedSpectrumCards = spectrumCards
            .Where(sc => !usedSpectrumCardIDs.Contains(sc.ID))
            .ToList();

        var newRound = new GameRound
        {
            GameSessionID = gameSessionID,
            SpectrumCardID = unusedSpectrumCards[random.Next(unusedSpectrumCards.Count)].ID
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

            var result = mapper.Map<GameRoundDTO>(newRound);

            result.RoundRoles = userRoles;

            return result;
        }

        return null;
    }

    /// <inheritdoc />
    public async Task<GameRoundGhostGuessDTO?> PerformGhostGuess(Guid userID, Guid gameSessionID, Guid gameRoundID,
        int targetOffset)
    {
        var gameSessionMember = await context.GameSessionMembers
            .Where(gsm => gsm.GameSessionID == gameSessionID)
            .Where(gsm => gsm.UserID == userID)
            .FirstOrDefaultAsync();
        var existingGuess = await context.GameRoundGhostGuesses
            .Where(gg => gg.GameSessionID == gameSessionID)
            .Where(gg => gg.GameRoundID == gameRoundID)
            .Where(gg => gg.UserID == userID)
            .FirstOrDefaultAsync();

        if (gameSessionMember == null) return null;

        if (existingGuess == null)
        {
            existingGuess = new GameRoundGhostGuess
            {
                GameSessionID = gameSessionID,
                UserID = userID,
                GameRoundID = gameRoundID,
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
    public async Task<GameRoundSelectorSelectionDTO?> SelectTarget(Guid userID, Guid gameSessionID, Guid gameRoundID,
        int targetOffset)
    {
        var gameSessionMember = await context.GameSessionMembers
            .Where(gsm => gsm.GameSessionID == gameSessionID)
            .Where(gsm => gsm.UserID == userID)
            .FirstOrDefaultAsync();
        var existingSelection = await context.GameRoundSelectorSelections
            .Where(gg => gg.GameSessionID == gameSessionID)
            .Where(gg => gg.GameRoundID == gameRoundID)
            .Where(gg => gg.UserID == userID)
            .FirstOrDefaultAsync();

        if (gameSessionMember == null) return null;

        if (existingSelection == null)
        {
            existingSelection = new GameRoundSelectorSelection
            {
                GameSessionID = gameSessionID,
                UserID = userID,
                GameRoundID = gameRoundID,
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
    public async Task<GameRoundOpposingTeamGuessDTO?> PerformOpposingTeamGuess(Guid userID, Guid gameSessionID,
        Guid gameRoundID, bool isLeft)
    {
        var gameSessionMember = await context.GameSessionMembers
            .Where(gsm => gsm.GameSessionID == gameSessionID)
            .Where(gsm => gsm.UserID == userID)
            .FirstOrDefaultAsync();
        var existingGuess = await context.GameRoundOpposingTeamGuesses
            .Where(gg => gg.GameSessionID == gameSessionID)
            .Where(gg => gg.GameRoundID == gameRoundID)
            .Where(gg => gg.UserID == userID)
            .FirstOrDefaultAsync();

        if (gameSessionMember == null) return null;

        if (existingGuess == null)
        {
            existingGuess = new GameRoundOpposingTeamGuess
            {
                GameSessionID = gameSessionID,
                UserID = userID,
                GameRoundID = gameRoundID,
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
    public async Task<GameRoundOpposingTeamSelectionDTO?> PerformOpposingTeamSelection(Guid userID, Guid gameSessionID,
        Guid gameRoundID, bool isLeft)
    {
        var gameSessionMember = await context.GameSessionMembers
            .Where(gsm => gsm.GameSessionID == gameSessionID)
            .Where(gsm => gsm.UserID == userID)
            .FirstOrDefaultAsync();
        var existingSelection = await context.GameRoundOpposingTeamSelections
            .Where(gg => gg.GameSessionID == gameSessionID)
            .Where(gg => gg.GameRoundID == gameRoundID)
            .Where(gg => gg.UserID == userID)
            .FirstOrDefaultAsync();

        if (gameSessionMember == null) return null;

        if (existingSelection == null)
        {
            existingSelection = new GameRoundOpposingTeamSelection
            {
                GameSessionID = gameSessionID,
                UserID = userID,
                GameRoundID = gameRoundID,
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
    public async Task<GameSessionMemberRoundRoleDTO?> GetRoundRole(Guid userID, Guid gameSessionID, Guid gameRoundID)
    {
        return await context.GameSessionMemberRoundRoles
            .Where(rr => rr.GameSessionID == gameSessionID)
            .Where(rr => rr.GameRoundID == gameRoundID)
            .Where(rr => rr.UserID == userID)
            .ProjectTo<GameSessionMemberRoundRoleDTO>(mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
    }

    /// <inheritdoc />
    public async Task<GameRoundDTO?> GetRound(Guid gameSessionID, Guid gameRoundID)
    {
        return await context.GameRounds
            .Where(rr => rr.GameSessionID == gameSessionID)
            .Where(rr => rr.ID == gameRoundID)
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
            .Where(gs => gs.GameSessionID == newRound.GameSessionID)
            .Where(gs => gs.Team != Team.NONE)
            .ToListAsync();

        var gameRoundRoles = new List<GameSessionMemberRoundRole>();

        foreach (var gameSessionMember in gameSessionMembers)
        {
            var roundRole = new GameSessionMemberRoundRole
            {
                GameSessionID = newRound.GameSessionID,
                UserID = gameSessionMember.UserID,
                Team = gameSessionMember.Team,
                GameRoundID = newRound.ID,
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
                .Where(rr => !HasUserHadRoleBefore(rr.UserID, TeamRole.PSYCHIC, previousRounds))
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
                .Where(rr => rr.UserID != psychicRoundRole.UserID)
                .Where(rr => !HasUserHadRoleBefore(rr.UserID, TeamRole.SELECTOR, previousRounds))
                .ToList();

            roundRoleListToUse = currentRoundRolesThatHaventBeenSelector.Any()
                ? currentRoundRolesThatHaventBeenSelector
                : roundTeamRoles.Where(rr => rr.UserID != psychicRoundRole.UserID).ToList();

            var selectorIndex = random.Next(roundRoleListToUse.Count);
            var selectorRoundRole = roundRoleListToUse[selectorIndex];
            selectorRoundRole.Role = TeamRole.SELECTOR;
        }

        // Make a random user a selector, prioritizing users that have not been selectors
        if (opposingTeamRoles.Any())
        {
            var currentRoundRolesThatHaventBeenSelector = opposingTeamRoles
                .Where(rr => !HasUserHadRoleBefore(rr.UserID, TeamRole.SELECTOR, previousRounds))
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

    private bool HasUserHadRoleBefore(Guid userID, TeamRole role, List<GameRound> previousRounds)
    {
        return previousRounds
            .Select(pr => pr.RoundRoles)
            .Any(roundRoles =>
                roundRoles
                    .Where(rr => rr.UserID == userID)
                    .Any(rr => rr.Role == role)
            );
    }
}