using Application.HubServices;
using Microsoft.AspNetCore.SignalR;
using Persistence.DataTransferObject;
using Persistence.Repositories;

namespace API.SignalR.HubServices;

public class GameRoundHubService : IGameRoundHubService
{
    private readonly IHubContext<GameSessionHub> gameSessionHub;
    private readonly IGameRoundRepository repository;

    public GameRoundHubService(IHubContext<GameSessionHub> gameSessionHub, IGameRoundRepository repository)
    {
        this.gameSessionHub = gameSessionHub;
        this.repository = repository;
    }

    public async Task NotifyRoundStart(Guid gameSessionID, GameRoundDTO gameRound)
    {
        await gameSessionHub.Clients.Group(GameSessionHub.GroupNameForAllGameSessionMembers(gameSessionID))
            .SendAsync("RoundStarted", gameRound);
    }

    public async Task NotifyTeamTurnGhostGuess(Guid gameSessionID, GameRoundGhostGuessDTO guess)
    {
        throw new NotImplementedException();
    }

    public async Task NotifyTeamTurnSelectorSelect(Guid gameSessionID, GameRoundSelectorSelectionDTO selection)
    {
        throw new NotImplementedException();
    }

    public async Task NotifyPsychicClue(Guid gameSessionID, GameRoundDTO gameRoundWithClue)
    {
        throw new NotImplementedException();
    }

    public async Task NotifyOpposingTeamGhostGuess(Guid gameSessionID, GameRoundGhostGuessDTO guess)
    {
        throw new NotImplementedException();
    }

    public async Task NotifyOpposingTeamSelectorSelect(Guid gameSessionID, GameRoundOpposingTeamSelectionDTO selection)
    {
        throw new NotImplementedException();
    }
}