using Domain;
using Microsoft.AspNetCore.Authorization;
using Persistence.Repositories;

namespace API.Authorization;

public class GameRoundRoleRequirement : IAuthorizationRequirement
{
    public GameRoundRoleRequirement()
    {
    }

    public GameRoundRoleRequirement(
        bool usersTeamHasTurn,
        TeamRole requiredRole)
    {
        UsersTeamHasTurn = usersTeamHasTurn;
        RequiredRole = requiredRole;
    }

    /// <summary>
    ///     Gets the value that the handler will use to determine if the user should be on the team who has or does not have a
    ///     turn.
    /// </summary>
    public bool UsersTeamHasTurn { get; init; }

    /// <summary>
    ///     Gets the value that the handler will use to determine if the user has a role.
    /// </summary>
    public TeamRole RequiredRole { get; init; }
}

public class HasGameRoundRoleHandler : BaseAuthorizationHandler<GameRoundRoleRequirement>
{
    private readonly IGameRoundRepository gameRoundRepository;
    private readonly IGameSessionMemberRepository gameSessionMemberRepository;

    public HasGameRoundRoleHandler(
        IHttpContextAccessor httpContextAccessor,
        IGameRoundRepository gameRoundRepository,
        IGameSessionMemberRepository gameSessionMemberRepository)
        : base(httpContextAccessor)
    {
        this.gameRoundRepository = gameRoundRepository;
        this.gameSessionMemberRepository = gameSessionMemberRepository;
    }

    protected override async Task<Task> HandleRequirementAsync(AuthorizationHandlerContext context,
        GameRoundRoleRequirement requirement)
    {
        var _gameSessionID = await GetValueFromRequest("gameSessionID");
        var _requesterID = GetRequesterID(context);

        if (_gameSessionID == null
            || _requesterID == null) return Task.CompletedTask;

        var gameSessionID = Guid.Parse(_gameSessionID);
        var userID = Guid.Parse(_requesterID);

        var gameRound = await gameRoundRepository.GetCurrentRound(gameSessionID);

        if (gameRound == null) return Task.CompletedTask;

        var userRole = gameRound.RoundRoles
            .FirstOrDefault(rr => rr.UserID == userID);

        if (userRole == null) return Task.CompletedTask;

        var team = userRole.Team;
        var role = userRole.Role;

        if (requirement.UsersTeamHasTurn && gameRound.TeamTurn != team)
            return Task.CompletedTask;
        if (!requirement.UsersTeamHasTurn && gameRound.TeamTurn == team)
            return Task.CompletedTask;

        if (role == requirement.RequiredRole) context.Succeed(requirement);

        return Task.CompletedTask;
    }
}