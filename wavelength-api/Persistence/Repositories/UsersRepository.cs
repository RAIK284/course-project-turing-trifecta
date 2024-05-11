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

    public async Task<UserDTO?> Get(Guid userId, CancellationToken cancellationToken = default)
    {
        var userIdString = userId.ToString();
        var user = await context.Users
            .Where(u => u.Id == userIdString)
            .ProjectTo<UserDTO>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return user;
    }

    public async Task<UserDTO?> UpdateProfile(UserDTO user)
    {
        var userIdString = user.Id.ToString();
        var result = await context.Users
            .Where(u => u.Id == userIdString)
            .FirstOrDefaultAsync();

        if (result == null)
        {
            return null;
        }

        result.UserName = user.UserName;
        result.AvatarId = user.AvatarId;
        await context.SaveChangesAsync();
        return mapper.Map<UserDTO>(result);
    }
}