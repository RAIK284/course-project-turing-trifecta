using Persistence.DataTransferObject;

namespace Persistence.Repositories;

public interface IGameRoundRepository
{
    /// <summary>
    ///     Starts a round. If this is the first round, a random team will be chosen to start.
    ///     If this is not the first round, the team that didn't previously go will have their turn.
    /// </summary>
    /// <param name="gameSessionID">The ID of the GameSession to make this round for.</param>
    /// <returns></returns>
    public Task<GameRoundDTO?> StartRound(Guid gameSessionID);

    /// <summary>
    ///     Performs a guess for a Ghost. This method will either create a new guess object or change the existing
    ///     object for the user for this round.
    /// </summary>
    /// <param name="userID">The ID of the user to create a guess under.</param>
    /// <param name="gameSessionID">The ID of the GameSession to make this guess for.</param>
    /// <param name="gameRoundID">The ID of the GameRound to make this guess for.</param>
    /// <param name="targetOffset">The degrees of offset from the origin to make this guess with.</param>
    public Task<GameRoundGhostGuessDTO?> PerformGhostGuess(
        Guid userID,
        Guid gameSessionID,
        Guid gameRoundID,
        int targetOffset);

    /// <summary>
    ///     Selects the target for the team whose turn it is in the current round.
    /// </summary>
    /// <param name="userID">The ID of the user who selected the target.</param>
    /// <param name="gameSessionID">The ID of the GameSession to make this guess for.</param>
    /// <param name="gameRoundID">The ID of the GameRound to make this guess for.</param>
    /// <param name="targetOffset">The degrees of offset from the origin to make this guess with.</param>
    public Task<GameRoundSelectorSelectionDTO?> SelectTarget(
        Guid userID,
        Guid gameSessionID,
        Guid gameRoundID,
        int targetOffset);

    /// <summary>
    ///     Performs a guess for a team member on the opposing team for the round.
    /// </summary>
    /// <param name="userID">The ID of the user to create a guess under.</param>
    /// <param name="gameSessionID">The ID of the GameSession to make this guess for.</param>
    /// <param name="gameRoundID">The ID of the GameRound to make this guess for.</param>
    /// <param name="isLeft">Represents whether the user chose left or right of the opposing team's target.</param>
    public Task<GameRoundOpposingTeamGuessDTO?> PerformOpposingTeamGuess(
        Guid userID,
        Guid gameSessionID,
        Guid gameRoundID,
        bool isLeft);

    /// <summary>
    ///     Selects left or right for the opposing team.
    /// </summary>
    /// <param name="userID">The ID of the user who selected left or right.</param>
    /// <param name="gameSessionID">The ID of the GameSession to make this selection for.</param>
    /// <param name="gameRoundID">The ID of the GameRound to make this selection for.</param>
    /// <param name="isLeft">Represents whether the team chose left or right of the opposing team's target.</param>
    public Task<GameRoundOpposingTeamSelectionDTO?> PerformOpposingTeamSelection(
        Guid userID,
        Guid gameSessionID,
        Guid gameRoundID,
        bool isLeft);

    /// <summary>
    ///     Updates the round with the clue the psychic entered.
    /// </summary>
    /// <param name="gameSessionID">The ID of the GameSession this clue was given for.</param>
    /// <param name="clue">The clue the psychic is giving.</param>
    /// <returns></returns>
    public Task<GameRoundDTO?> PsychicGiveClue(Guid gameSessionID, string clue);

    /// <summary>
    ///     Gets a Game Round.
    /// </summary>
    /// <param name="gameSessionID">The ID of the GameSession to get this round for.</param>
    /// <param name="gameRoundID">The ID of the GameRound to find the round by.</param>
    /// <returns></returns>
    public Task<GameRoundDTO?> GetRound(Guid gameSessionID, Guid gameRoundID);

    /// <summary>
    ///     Gets the latest, current round.
    /// </summary>
    /// <param name="gameSessionID">The ID of the GameSession to grab to the round for.</param>
    /// <returns></returns>
    public Task<GameRoundDTO?> GetCurrentRound(Guid gameSessionID);

    /// <summary>
    ///     Gets the round role for a user for a round.
    /// </summary>
    /// <param name="userID">The ID of the user.</param>
    /// <param name="gameSessionID">The ID of the GameSession.</param>
    /// <param name="gameRoundID">The ID of the GameRound.</param>
    /// <returns>A DTO representing the round role or null if none was found.</returns>
    public Task<GameSessionMemberRoundRoleDTO?> GetRoundRole(
        Guid userID,
        Guid gameSessionID,
        Guid gameRoundID);
}