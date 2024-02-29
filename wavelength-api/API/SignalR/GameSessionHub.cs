using AutoMapper;
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
    public override Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        
        return base.OnConnectedAsync();
    }
}