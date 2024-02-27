﻿using Application.Core;
using Application.GameSessions;
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
        services.AddCors(opt => opt.AddPolicy("CorsPolicy",
            policy => policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000")));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Get.Handler).Assembly));
        services.AddAutoMapper(typeof(MappingProfiles).Assembly);
        services.AddAutoMapper(typeof(UserMappingProfiles).Assembly);
        services.AddScoped<IUsersRepository, UsersRepository>();

        return services;
    }
}