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
    /// Gets the value that the handler will use to determine if the user should be on the team who has or does not have a turn.
    /// </summary>
    public bool UsersTeamHasTurn { get; init; } 
    
    /// <summary>
    /// Gets the value that the handler will use to determine if the user has a role.
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

    protected async override Task<Task> HandleRequirementAsync(AuthorizationHandlerContext context,
        GameRoundRoleRequirement requirement)
    {
        string? _gameSessionID = await GetValueFromRequest("gameSessionID");
        string? _gameRoundID = await GetValueFromRequest("gameRoundID");
        string? _team = await GetValueFromRequest("team");
        string? _requesterID = GetRequesterID(context);

        if (_gameSessionID == null
            || _gameRoundID == null
            || _team == null
            || _requesterID == null) return Task.CompletedTask;

        Guid gameSessionID = Guid.Parse(_gameSessionID);
        Guid gameRoundID = Guid.Parse(_gameRoundID);
        Guid userID = Guid.Parse(_requesterID);
        Team? team = TeamHelper.GetTeamFromString(_team);

        var gameRound = await gameRoundRepository.GetRound(gameSessionID, gameRoundID);

        if (gameRound == null) 
            return Task.CompletedTask;

        if (requirement.UsersTeamHasTurn && gameRound.TeamTurn != team) 
            return Task.CompletedTask;
        if (!requirement.UsersTeamHasTurn && gameRound.TeamTurn == team)
            return Task.CompletedTask;

        var gameSessionMember = await gameSessionMemberRepository.Get(userID, gameSessionID);

        if (gameSessionMember == null)
            return Task.CompletedTask;

        if (gameSessionMember.Team != team)
            return Task.CompletedTask;

        var gameSessionRoundRole =
            await gameRoundRepository.GetRoundRole(userID, gameSessionID, gameRoundID);

        if (gameSessionRoundRole == null) return Task.CompletedTask;

        if (gameSessionRoundRole.Role == requirement.RequiredRole)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}