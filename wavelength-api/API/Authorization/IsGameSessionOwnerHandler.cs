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
        string? _gameSessionId = await GetValueFromRequest("gameSessionId");
        string? _requesterId = GetRequesterId(context);

        if (_gameSessionId == null || _requesterId == null) return Task.CompletedTask;

        Guid gameSessionId = Guid.Parse(_gameSessionId);
        Guid userId = Guid.Parse(_requesterId);

        var gameSession = await gameSessionRepository.Get(gameSessionId);

        if (gameSession == null) return Task.CompletedTask;

        if (gameSession.OwnerId == userId)
        {
            context.Succeed(requirement);
        }
        
        return Task.CompletedTask;
    }
}