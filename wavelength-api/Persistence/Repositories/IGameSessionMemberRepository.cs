using Domain;
using Persistence.DataTransferObject;

namespace Persistence.Repositories;

public interface IGameSessionMemberRepository
{
    /// <summary>
    /// Allows a user to join or switch a team.
    /// </summary>
    /// <param name="userID">The ID of the user who is trying to join a team.</param>
    /// <param name="gameSessionID">The ID of the GameSession for which the user is trying to join a team in.</param>
    /// <param name="team">The team the user is trying to join.</param>
    /// <returns></returns>
    public Task<GameSessionMemberDTO?> JoinTeam(Guid userID, Guid gameSessionID, Team team);

    /// <summary>
    /// Gets the member entity for a user in a game session.
    /// </summary>
    /// <param name="userID">The ID of the user for this request.</param>
    /// <param name="gameSessionID">The ID of the GameSession for which to grab the member entity.</param>
    /// <returns></returns>
    public Task<GameSessionMemberDTO?> Get(Guid userID, Guid gameSessionID);
}