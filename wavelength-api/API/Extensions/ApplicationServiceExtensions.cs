using API.SignalR.HubServices;
using Application.GameSessions;
using Application.HubServices;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Core;
using Persistence.Repositories;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration config
    )
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddDbContext<DataContext>(opt => opt.UseSqlite(config.GetConnectionString("DefaultConnection")));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Get.Handler).Assembly));
        services.AddAutoMapper(typeof(MappingProfiles).Assembly);
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IGameSessionMemberRepository, GameSessionMemberRepository>();
        services.AddScoped<IGameSessionRepository, GameSessionRepository>();
        services.AddScoped<IGameRoundRepository, GameRoundRepository>();
        services.AddScoped<ISpectrumCardRepository, SpectrumCardRepository>();
        services.AddSignalR();
        services.AddScoped<IGameRoundHubService, GameRoundHubService>();
        services.AddScoped<IGameSessionHubService, GameSessionHubService>();

        return services;
    }
}