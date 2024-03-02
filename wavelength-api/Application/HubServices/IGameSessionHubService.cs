using Persistence.DataTransferObject;

namespace Application.HubServices;

public interface IGameSessionHubService
{
    public Task NotifyUserJoined(Guid gameSessionID, GameSessionMemberDTO member);

    public Task NotifyUserLeft(Guid gameSessionID, Guid userID);
}