using Domain;
using Persistence.DataTransferObject;

namespace Persistence.Repositories;

public interface IGameSessionRepository
{
    public Task<GameSessionDTO?> Create(Guid ownerID);

    public Task<GameSessionDTO?> Get(Guid gameSessionID);

    public Task<GameSessionMemberDTO?> Join(Guid gameSessionID, Guid userID);

    public Task<bool> Leave(Guid gameSessionID, Guid userID);

    public Task<bool> End(Guid gameSessionID);

    public Task<bool> Start(Guid gameSessionID);

    public Task<GameSessionDTO?> GetActiveSession(Guid userID);
}