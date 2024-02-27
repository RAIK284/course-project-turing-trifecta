using Microsoft.AspNetCore.Authorization;
using Persistence.Repositories;

namespace API.Authorization;

public abstract class BaseAuthorizationHandler<T> : AuthorizationHandler<T> where T : IAuthorizationRequirement, new()
{
    private readonly IGameSessionRepository gameSessionsRepository;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly IUsersRepository usersRepository;

    protected BaseAuthorizationHandler(IHttpContextAccessor httpContextAccessor, IUsersRepository usersRepository,
        IGameSessionRepository gameSessionsRepository)
    {
        this.httpContextAccessor = httpContextAccessor;
        this.usersRepository = usersRepository;
        this.gameSessionsRepository = gameSessionsRepository;
    }
}