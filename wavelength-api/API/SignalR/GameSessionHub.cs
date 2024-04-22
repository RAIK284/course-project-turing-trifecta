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

    public static string GroupNameForIndividual(Guid userId, Guid gameSessionId)
    {
        return $"{gameSessionId}-Member-{userId}";
    }

    public static string GroupNameForAllGameSessionMembers(Guid gameSessionId)
    {
        return $"{gameSessionId}-AllMembers";
    }

    [Authorize(Policy = "IsGameSessionMember")]
    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var _gameSessionId = httpContext.Request.Query["gameSessionId"];
        var canParseGameSessionId = Guid.TryParse(_gameSessionId, out var gameSessionId);
        var canParseUserId = Guid.TryParse(Context.UserIdentifier, out var userId);

        if (gameSessionId == null || !canParseGameSessionId || !canParseUserId) return;

        await JoinGameSessionGroup(userId, gameSessionId);

        await base.OnConnectedAsync();
    }

    private async Task JoinGameSessionGroup(Guid userId, Guid gameSessionId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, GroupNameForAllGameSessionMembers(gameSessionId));
        await Groups.AddToGroupAsync(Context.ConnectionId, GroupNameForIndividual(userId, gameSessionId));
    }
}