using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence;

public class Seed
{
    public static async Task SeedData(DataContext context, UserManager<User> userManager)
    {
        var ID_User_Peyton = Guid.Parse("5e5136b2-3445-4286-9121-084123021882");
        var JoinCode_GameSession_1 = "193245";
        var ID_GameSession_1 = Guid.Parse("5e5136b8-5845-4256-92c1-084237021882");
        var User_Password = "Wavelength1";

        if (!userManager.Users.Any())
        {
            var users = new List<User>
            {
                new()
                {
                    Id = ID_User_Peyton.ToString(),
                    Email = "peyton@wavelength.net",
                    UserName = "P_Sizzle"
                }
            };

            foreach (var user in users) await userManager.CreateAsync(user, User_Password);
        }

        if (context.GameSessions.Any()) return;

        var gameSessions = new List<GameSession>
        {
            new()
            {
                ID = ID_GameSession_1,
                JoinCode = JoinCode_GameSession_1,
                StartTime = DateTime.Now,
                OwnerID = ID_User_Peyton
            }
        };

        context.AddRangeAsync(gameSessions);
        await context.SaveChangesAsync();
    }
}