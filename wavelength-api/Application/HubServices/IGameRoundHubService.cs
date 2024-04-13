﻿using Persistence.DataTransferObject;

namespace Application.HubServices;

public interface IGameRoundHubService
{
    public Task NotifyRoundStart(Guid gameSessionId, GameRoundDTO gameRound);

    public Task NotifyTeamTurnGhostGuess(Guid gameSessionId, GameRoundGhostGuessDTO guess);

    public Task NotifyTeamTurnSelectorSelect(Guid gameSessionId, GameRoundSelectorSelectionDTO selection);

    public Task NotifyPsychicClue(Guid gameSessionId, GameRoundDTO gameRoundWithClue);

    public Task NotifyOpposingTeamGhostGuess(Guid gameSessionId, GameRoundGhostGuessDTO guess);

    public Task NotifyOpposingTeamSelectorSelect(Guid gameSessionId, GameRoundOpposingTeamSelectionDTO selection);
}