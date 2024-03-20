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
        var _userID = GetRequesterID(context);

        if (_userID == null) return Task.CompletedTask;

        if (!Guid.TryParse(_userID, out var userID))
            return Task.CompletedTask;

        var gameSessionMember = await GetGameSessionMemberForRequest(userID);

        if (gameSessionMember != null && !requirement.SucceedIfUserIsNotMember) context.Succeed(requirement);

        if (gameSessionMember == null && requirement.SucceedIfUserIsNotMember) context.Succeed(requirement);

        return Task.CompletedTask;
    }

    private async Task<GameSessionMemberDTO?> GetGameSessionMemberForRequest(Guid userID)
    {
        var _gameSessionID = await GetValueFromRequest("gameSessionID");
        var _gameSessionJoinCode = await GetValueFromRequest("joinCode");

        if (_gameSessionID != null && Guid.TryParse(_gameSessionID, out var gameSessionID))
            return await gameSessionMemberRepository.Get(userID, gameSessionID);

        if (_gameSessionJoinCode != null)
        {
            var gameSession = await gameSessionRepository.GetByJoinCode(_gameSessionJoinCode);

            if (gameSession != null) return await gameSessionMemberRepository.Get(userID, gameSession.ID);
        }

        return null;
    }
}