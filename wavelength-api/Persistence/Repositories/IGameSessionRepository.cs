using Domain;

namespace Persistence.Repositories;

public interface IGameSessionRepository
{
    public GameSession create(Guid ownerID);

    public GameSession get(Guid gameSessionID);

    public GameSession join(Guid gameSessionID, Guid userID);

    public GameSession leave(Guid gameSessionID, Guid userID);
}