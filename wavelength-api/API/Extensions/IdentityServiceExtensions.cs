using System.Text;
using API.Authorization;
using API.Services;
using Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Persistence;

namespace API.Extensions;

public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
    {
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequiredLength = 8;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<DataContext>();

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(config["TokenKey"]));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        _ = services.AddAuthorization(opt =>
        {
            opt.AddPolicy(AuthPolicy.IsGameSessionMember,
                policy => { policy.Requirements.Add(new GameMemberHandlerRequirement(true)); });
            opt.AddPolicy(AuthPolicy.IsGhostOnTeamTurn,
                policy => { policy.Requirements.Add(new GameRoundRoleRequirement(true, TeamRole.GHOST)); });
            opt.AddPolicy(AuthPolicy.IsSelectorOnTeamTurn,
                policy => { policy.Requirements.Add(new GameRoundRoleRequirement(true, TeamRole.SELECTOR)); });
            opt.AddPolicy(AuthPolicy.IsPsychicOnTeamTurn,
                policy => { policy.Requirements.Add(new GameRoundRoleRequirement(true, TeamRole.PSYCHIC)); });
            opt.AddPolicy(AuthPolicy.HasNoRoleOnTeamTurn,
                policy => { policy.Requirements.Add(new GameRoundRoleRequirement(true, TeamRole.NONE)); });
            opt.AddPolicy(AuthPolicy.IsGhostOnOpposingTeam,
                policy => { policy.Requirements.Add(new GameRoundRoleRequirement(false, TeamRole.GHOST)); });
            opt.AddPolicy(AuthPolicy.IsSelectorOnOpposingTeam,
                policy => { policy.Requirements.Add(new GameRoundRoleRequirement(false, TeamRole.SELECTOR)); });
            opt.AddPolicy(AuthPolicy.HasNoRoleOnOpposingTeam,
                policy => { policy.Requirements.Add(new GameRoundRoleRequirement(false, TeamRole.NONE)); });
            opt.AddPolicy(AuthPolicy.IsGameSessionOwner,
                policy => { policy.Requirements.Add(new GameSessionOwnerRequirement()); });
        });
        services.AddTransient<IAuthorizationHandler, IsGameMemberHandler>();
        services.AddTransient<IAuthorizationHandler, HasGameRoundRoleHandler>();
        services.AddTransient<IAuthorizationHandler, IsGameSessionOwnerHandler>();
        services.AddScoped<TokenService>();

        return services;
    }
}