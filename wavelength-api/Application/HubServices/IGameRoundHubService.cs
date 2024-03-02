using Persistence.DataTransferObject;

namespace Application.HubServices;

public interface IGameRoundHubService
{
    public Task NotifyRoundStart(Guid gameSessionID, GameRoundDTO gameRound);

    public Task NotifyTeamTurnGhostGuess(Guid gameSessionID, GameRoundGhostGuessDTO guess);

    public Task NotifyTeamTurnSelectorSelect(Guid gameSessionID, GameRoundSelectorSelectionDTO selection);

    public Task NotifyPsychicClue(Guid gameSessionID, GameRoundDTO gameRoundWithClue);

    public Task NotifyOpposingTeamGhostGuess(Guid gameSessionID, GameRoundGhostGuessDTO guess);

    public Task NotifyOpposingTeamSelectorSelect(Guid gameSessionID, GameRoundOpposingTeamSelectionDTO selection);
}