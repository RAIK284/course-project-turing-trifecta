using Microsoft.AspNetCore.Authorization;
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

    public IsGameMemberHandler(
        IHttpContextAccessor httpContextAccessor,
        IGameSessionMemberRepository gameSessionMemberRepository)
        : base(httpContextAccessor)
    {
        this.gameSessionMemberRepository = gameSessionMemberRepository;
    }

    protected override async Task<Task> HandleRequirementAsync(AuthorizationHandlerContext context,
        GameMemberHandlerRequirement requirement)
    {
        var _userID = GetRequesterID(context);
        var _gameSessionID = await GetValueFromRequest("gameSessionID");

        if (_userID == null || _gameSessionID == null) return Task.CompletedTask;

        if (!Guid.TryParse(_userID, out var userID) || !Guid.TryParse(_gameSessionID, out var gameSessionID))
            return Task.CompletedTask;

        var gameSessionMember = await gameSessionMemberRepository.Get(userID, gameSessionID);

        if (gameSessionMember != null && !requirement.SucceedIfUserIsNotMember) context.Succeed(requirement);

        if (gameSessionMember == null && requirement.SucceedIfUserIsNotMember) context.Succeed(requirement);

        return Task.CompletedTask;
    }
}