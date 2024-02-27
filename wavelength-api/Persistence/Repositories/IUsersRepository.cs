using Persistence.DataTransferObject;

namespace Persistence.Repositories;

public interface IUsersRepository
{
    public Task<UserDTO?> Get(Guid userID, CancellationToken cancellationToken = default);
}