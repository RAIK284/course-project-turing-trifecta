using Persistence.DataTransferObject;

namespace Persistence.Repositories;

public interface IGameSessionRepository
{
    public Task<GameSessionDTO?> Create(Guid ownerId);

    public Task<GameSessionDTO?> Get(Guid gameSessionId);

    public Task<GameSessionMemberDTO?> Join(Guid gameSessionId, Guid userId);

    public Task<GameSessionDTO?> GetByJoinCode(string joinCode);

    public Task<bool> Leave(Guid gameSessionId, Guid userId);

    public Task<bool> End(Guid gameSessionId);

    public Task<bool> Start(Guid gameSessionId);

    public Task<GameSessionDTO?> GetActiveSession(Guid userId);
}