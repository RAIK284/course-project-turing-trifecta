using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR;

public class GameSessionHub : Hub
{
    private readonly IMediator mediator;

    public GameSessionHub(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public static string GroupNameForTeamOne(Guid gameSessionID)
    {
        return $"${gameSessionID}-TeamOne";
    }

    public static string GroupNameForTeamTwo(Guid gameSessionID)
    {
        return $"${gameSessionID}-TeamTwo";
    }

    public static string GroupNameForAllGameSessionMembers(Guid gameSessionID)
    {
        return $"${gameSessionID}-AllMembers";
    }

    [Authorize(Policy = "IsGameSessionMember")]
    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var _gameSessionID = httpContext.Request.Query["gameSessionID"];
        var canParseGameSessionID = Guid.TryParse(_gameSessionID, out var gameSessionID);
        var canParseUserID = Guid.TryParse(Context.UserIdentifier, out var userID);

        if (gameSessionID == null || !canParseGameSessionID || !canParseUserID) return;

        base.OnConnectedAsync();
    }

    private async Task JoinGameSessionGroup(Guid gameSessionID)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, GroupNameForAllGameSessionMembers(gameSessionID));
    }

    private async Task JoinGameSessionTeamOne(Guid gameSessionID)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, GroupNameForTeamOne(gameSessionID));
    }

    private async Task JoinGameSessionTeamTwo(Guid gameSessionID)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, GroupNameForTeamTwo(gameSessionID));
    }
}