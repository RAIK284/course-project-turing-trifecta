using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Persistence.Repositories;

namespace API.Authorization;

public class GameMemberHandlerRequirement : IAuthorizationRequirement
{
    public GameMemberHandlerRequirement()
    {
        
    }

    public GameMemberHandlerRequirement(
        bool gameSessionMember)
    {
        GameSessionMember = gameSessionMember;
    }
    
    /// <summary>
    /// Gets the value indicating whether the handlers should check that the user is in the requested game session.
    /// </summary>
    public bool GameSessionMember { get; init; }
}

public class IsGameMemberHandler : BaseAuthorizationHandler<GameMemberHandlerRequirement>
{

    private readonly IGameSessionMemberRepository gameSessionMemberRepository;
    
    public IsGameMemberHandler(
        IHttpContextAccessor httpContextAccessor, 
        IGameSessionMemberRepository gameSessionMemberRepository)
        : base(httpContextAccessor)
    {
        this.gameSessionMemberRepository = gameSessionMemberRepository;
    }

    protected async override Task<Task> HandleRequirementAsync(AuthorizationHandlerContext context,
        GameMemberHandlerRequirement requirement)
    {
        if (!requirement.GameSessionMember) return Task.CompletedTask;

        string stringRequestorID = context.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        string? gameSessionID = await GetValueFromRequest("gameSessionID");

        if (gameSessionID == null) return Task.CompletedTask;

        var gameSessionMember = await gameSessionMemberRepository.Get(
            Guid.Parse(stringRequestorID),
            Guid.Parse(gameSessionID)
            );

        if (gameSessionMember != null) context.Succeed(requirement);

        return Task.CompletedTask;
    }
}