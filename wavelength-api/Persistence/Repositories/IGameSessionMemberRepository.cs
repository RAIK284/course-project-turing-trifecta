using Domain;
using Persistence.DataTransferObject;

namespace Persistence.Repositories;

public interface IGameSessionMemberRepository
{
    /// <summary>
    ///     Allows a user to join or switch a team.
    /// </summary>
    /// <param name="userId">The Id of the user who is trying to join a team.</param>
    /// <param name="gameSessionId">The Id of the GameSession for which the user is trying to join a team in.</param>
    /// <param name="team">The team the user is trying to join.</param>
    /// <returns></returns>
    public Task<GameSessionMemberDTO?> JoinTeam(Guid userId, Guid gameSessionId, Team team);

    /// <summary>
    ///     Gets the member entity for a user in a game session.
    /// </summary>
    /// <param name="userId">The Id of the user for this request.</param>
    /// <param name="gameSessionId">The Id of the GameSession for which to grab the member entity.</param>
    /// <returns></returns>
    public Task<GameSessionMemberDTO?> Get(Guid userId, Guid gameSessionId);

    /// <summary>
    ///     Gets all members for a game session.
    /// </summary>
    /// <param name="gameSessionId">The Id of the GameSession for which to grab all members.</param>
    /// <returns></returns>
    public Task<List<GameSessionMemberDTO>> GetAll(Guid gameSessionId);

    /// <summary>
    ///     Assigns all players who have no team to a team.
    /// </summary>
    /// <param name="gameSessionId">The Id of the GameSession for which to assign teams.</param>
    /// <returns></returns>
    public Task<List<GameSessionMemberDTO>> AssignTeamlessPlayersToTeam(Guid gameSessionId);
}