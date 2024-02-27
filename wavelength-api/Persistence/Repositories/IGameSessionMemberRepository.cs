using Domain;
using Persistence.DataTransferObject;

namespace Persistence.Repositories;

public interface IGameSessionMemberRepository
{
    public GameSessionMemberDTO? updateTeamStatus(Guid userID, Team team, TeamRole role);
}