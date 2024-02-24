using Domain;

namespace Persistence;

public class Seed
{
    public static async Task SeedData(DataContext context)
    {
        if (context.GameSessions.Any()) return;

        var ID_GameSession_1 = "193245";
        Guid ID_User_1 = Guid.NewGuid();

        var gameSessions = new List<GameSession>
        {
            new GameSession()
            {
                JoinCode = ID_GameSession_1,
                StartTime = DateTime.Now,
                OwnerID = ID_User_1
            }
        };

        context.AddRangeAsync(gameSessions);
        context.SaveChangesAsync();
    }
}