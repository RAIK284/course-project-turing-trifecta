using Microsoft.AspNetCore.Authorization;
using Persistence.DataTransferObject;
using Persistence.Repositories;

namespace API.Authorization;

public class GameMemberHandlerRequirement : IAuthorizationRequirement
{
    public GameMemberHandlerRequirement()
    {
    }

    public GameMemberHandlerRequirement(
        bool succeedIfUserIsNotMember)
    {
        SucceedIfUserIsNotMember = succeedIfUserIsNotMember;
    }

    /// <summary>
    ///     Gets the value indicating whether the handler should succeed if the user isn't a part of the game.
    /// </summary>
    public bool SucceedIfUserIsNotMember { get; set; }
}

public class IsGameMemberHandler : BaseAuthorizationHandler<GameMemberHandlerRequirement>
{
    private readonly IGameSessionMemberRepository gameSessionMemberRepository;
    private readonly IGameSessionRepository gameSessionRepository;

    public IsGameMemberHandler(
        IHttpContextAccessor httpContextAccessor,
        IGameSessionMemberRepository gameSessionMemberRepository,
        IGameSessionRepository gameSessionRepository)
        : base(httpContextAccessor)
    {
        this.gameSessionMemberRepository = gameSessionMemberRepository;
        this.gameSessionRepository = gameSessionRepository;
    }

    protected override async Task<Task> HandleRequirementAsync(AuthorizationHandlerContext context,
        GameMemberHandlerRequirement requirement)
    {
        var _userId = GetRequesterId(context);

        if (_userId == null) return Task.CompletedTask;

        if (!Guid.TryParse(_userId, out var userId))
            return Task.CompletedTask;

        var gameSessionMember = await GetGameSessionMemberForRequest(userId);

        if (gameSessionMember != null && !requirement.SucceedIfUserIsNotMember) context.Succeed(requirement);

        if (gameSessionMember == null && requirement.SucceedIfUserIsNotMember) context.Succeed(requirement);

        return Task.CompletedTask;
    }

    private async Task<GameSessionMemberDTO?> GetGameSessionMemberForRequest(Guid userId)
    {
        var _gameSessionId = await GetValueFromRequest("gameSessionId");
        var _gameSessionJoinCode = await GetValueFromRequest("joinCode");

        if (_gameSessionId != null && Guid.TryParse(_gameSessionId, out var gameSessionId))
            return await gameSessionMemberRepository.Get(userId, gameSessionId);

        if (_gameSessionJoinCode != null)
        {
            var gameSession = await gameSessionRepository.GetByJoinCode(_gameSessionJoinCode);

            if (gameSession != null) return await gameSessionMemberRepository.Get(userId, gameSession.Id);
        }

        return null;
    }
}