using Persistence.DataTransferObject;

namespace Persistence.Repositories;

public interface IUsersRepository
{
    public Task<UserDTO?> Get(Guid userId, CancellationToken cancellationToken = default);
    public Task<UserDTO?> UpdateProfile(UserDTO user);
}