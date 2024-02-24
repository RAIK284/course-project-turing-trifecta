using Domain;

namespace Persistence;

public class Seed
{
    public static async Task SeedData(DataContext context)
    {
        if (context.GameSessions.Any()) return;

        var JoinCode_GameSession_1 = "193245";
        var ID_User_1 = Guid.NewGuid();
        var ID_GameSession_1 = Guid.Parse("5e5136b8-5845-4256-92c1-084237021882");

        var gameSessions = new List<GameSession>
        {
            new GameSession()
            {
                ID = ID_GameSession_1,
                JoinCode = JoinCode_GameSession_1,
                StartTime = DateTime.Now,
                OwnerID = ID_User_1
            }
        };

        context.AddRangeAsync(gameSessions);
        context.SaveChangesAsync();
    }
}