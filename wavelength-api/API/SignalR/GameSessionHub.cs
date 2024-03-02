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

    public static string GroupNameForIndividual(Guid userID, Guid gameSessionID)
    {
        return $"{gameSessionID}-Member-{userID}";
    }

    public static string GroupNameForAllGameSessionMembers(Guid gameSessionID)
    {
        return $"{gameSessionID}-AllMembers";
    }

    [Authorize(Policy = "IsGameSessionMember")]
    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var _gameSessionID = httpContext.Request.Query["gameSessionID"];
        var canParseGameSessionID = Guid.TryParse(_gameSessionID, out var gameSessionID);
        var canParseUserID = Guid.TryParse(Context.UserIdentifier, out var userID);

        if (gameSessionID == null || !canParseGameSessionID || !canParseUserID) return;

        await JoinGameSessionGroup(userID, gameSessionID);

        await base.OnConnectedAsync();
    }

    private async Task JoinGameSessionGroup(Guid userID, Guid gameSessionID)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, GroupNameForAllGameSessionMembers(gameSessionID));
        await Groups.AddToGroupAsync(Context.ConnectionId, GroupNameForIndividual(userID, gameSessionID));
    }
}