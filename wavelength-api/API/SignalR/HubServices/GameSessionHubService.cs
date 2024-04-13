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

    public async Task NotifyUserJoinedTeam(Guid gameSessionId, GameSessionMemberDTO member)
    {
        await gameSessionHub.Clients.Group(GameSessionHub.GroupNameForAllGameSessionMembers(gameSessionId))
            .SendAsync("UserSwitchedTeams", member);
    }

    public async Task NotifyUserJoined(Guid gameSessionId, GameSessionMemberDTO member)
    {
        await gameSessionHub.Clients.Group(GameSessionHub.GroupNameForAllGameSessionMembers(gameSessionId))
            .SendAsync("UserJoinedGameSession", member);
    }

    public async Task NotifyUserLeft(Guid gameSessionId, Guid userId)
    {
        await gameSessionHub.Clients.Group(GameSessionHub.GroupNameForAllGameSessionMembers(gameSessionId))
            .SendAsync("UserJoinedGameSession", userId);
    }
}