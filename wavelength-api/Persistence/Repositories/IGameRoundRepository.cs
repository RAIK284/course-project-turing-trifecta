﻿using Domain;
using Persistence.DataTransferObject;

namespace Persistence.Repositories;

public interface IGameRoundRepository
{

    /// <summary>
    /// Starts a round. If this is the first round, a random team will be chosen to start.
    /// If this is not the first round, the team that didn't previously go will have their turn.
    /// </summary>
    /// <param name="gameSessionID">The ID of the GameSession to make this round for.</param>
    /// <returns></returns>
    public Task<GameRoundDTO?> StartRound(Guid gameSessionID);
    
    /// <summary>
    /// Performs a guess for a Ghost. This method will either create a new guess object or change the existing
    /// object for the user for this round.
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
    /// Selects the target for the team whose turn it is in the current round.
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
    /// Performs a guess for a team member on the opposing team for the round.
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
    /// Selects left or right for the opposing team.
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
    
    
}