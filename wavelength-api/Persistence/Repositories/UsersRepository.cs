using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Persistence.DataTransferObject;

namespace Persistence.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly DataContext context;
    private readonly IMapper mapper;

    public UsersRepository(DataContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<UserDTO?> Get(Guid userID, CancellationToken cancellationToken = default)
    {
        var userIDString = userID.ToString();
        var user = await context.Users
            .Where(u => u.Id == userIDString)
            .ProjectTo<UserDTO>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return user;
    }
}