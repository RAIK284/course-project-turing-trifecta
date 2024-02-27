using Microsoft.AspNetCore.Authorization;
using Persistence.Repositories;

namespace API.Authorization;

public class GameSessionOwnerRequirement : IAuthorizationRequirement
{
    public GameSessionOwnerRequirement()
    {
        
    }
}

public class IsGameSessionOwnerHandler : BaseAuthorizationHandler<GameSessionOwnerRequirement>
{

    private readonly IGameSessionRepository gameSessionRepository;
    
    public IsGameSessionOwnerHandler(
        IHttpContextAccessor httpContextAccessor,
        IGameSessionRepository gameSessionRepository)
        : base(httpContextAccessor)
    {
        this.gameSessionRepository = gameSessionRepository;
    }


    protected async override Task<Task> HandleRequirementAsync(AuthorizationHandlerContext context,
        GameSessionOwnerRequirement requirement)
    {
        string? _gameSessionID = await GetValueFromRequest("gameSessionID");
        string? _requesterID = GetRequesterID(context);

        if (_gameSessionID == null || _requesterID == null) return Task.CompletedTask;

        Guid gameSessionID = Guid.Parse(_gameSessionID);
        Guid userID = Guid.Parse(_requesterID);

        var gameSession = await gameSessionRepository.Get(gameSessionID);

        if (gameSession == null) return Task.CompletedTask;

        if (gameSession.OwnerID == userID)
        {
            context.Succeed(requirement);
        }
        
        return Task.CompletedTask;
    }
}