using Persistence.DataTransferObject;

namespace Application.HubServices;

public interface IGameSessionHubService
{
    public Task NotifyUserJoined(Guid gameSessionId, GameSessionMemberDTO member);

    public Task NotifyUserLeft(Guid gameSessionId, Guid userId);

    public Task NotifyUserJoinedTeam(Guid gameSessionId, GameSessionMemberDTO member);
}