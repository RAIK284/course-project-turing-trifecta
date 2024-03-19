using Application.HubServices;
using Microsoft.AspNetCore.SignalR;
using Persistence.DataTransferObject;

namespace API.SignalR.HubServices;

public class GameSessionHubService : IGameSessionHubService
{
    private readonly IHubContext<GameSessionHub> gameSessionHub;

    public GameSessionHubService(IHubContext<GameSessionHub> gameSessionHub)
    {
        this.gameSessionHub = gameSessionHub;
    }

    public async Task NotifyUserJoinedTeam(Guid gameSessionID, GameSessionMemberDTO member)
    {
        await gameSessionHub.Clients.Group(GameSessionHub.GroupNameForAllGameSessionMembers(gameSessionID))
            .SendAsync("UserSwitchedTeams", member);
    }

    public async Task NotifyUserJoined(Guid gameSessionID, GameSessionMemberDTO member)
    {
        await gameSessionHub.Clients.Group(GameSessionHub.GroupNameForAllGameSessionMembers(gameSessionID))
            .SendAsync("UserJoinedGameSession", member);
    }

    public async Task NotifyUserLeft(Guid gameSessionID, Guid userID)
    {
        await gameSessionHub.Clients.Group(GameSessionHub.GroupNameForAllGameSessionMembers(gameSessionID))
            .SendAsync("UserJoinedGameSession", userID);
    }
}