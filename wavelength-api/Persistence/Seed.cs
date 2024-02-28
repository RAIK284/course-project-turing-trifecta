using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence;

public class Seed
{
    public static async Task SeedData(DataContext context, UserManager<User> userManager)
    {
        var ID_User_Peyton = "33c91baa-77f3-44df-8907-53585a595f92";
        var ID_User_Nathan = "18029e32-f118-47ce-8470-5e267440ed75";
        var ID_User_Jake = "d2b4ce48-7df8-4c59-b1f0-0470a1261040";
        var ID_User_Harith = "7f7a2345-bc69-428e-ac23-9119d30e1a20";
        var ID_User_Kevin = "def3e622-e958-468d-b366-8596b285c3c8";
        var User_Password = "Wavelength1";

        if (!userManager.Users.Any())
        {
            var users = new List<User>
            {
                new()
                {
                    Id = ID_User_Peyton,
                    Email = "peyton@wavelength.net",
                    UserName = "P_Sizzle"
                },
                new()
                {
                    Id = ID_User_Nathan,
                    Email = "nathan@wavelength.net",
                    UserName = "Nathaniel"
                },
                new()
                {
                    Id = ID_User_Jake,
                    Email = "jake@wavelength.net",
                    UserName = "Jakester"
                },
                new()
                {
                    Id = ID_User_Harith,
                    Email = "harith@wavelength.net",
                    UserName = "HHarith_"
                },
                new()
                {
                    Id = ID_User_Kevin,
                    Email = "kevin@wavelength.net",
                    UserName = "Keviq"
                }
            };

            foreach (var user in users)
                if (await userManager.FindByEmailAsync(user.Email) == null)
                    await userManager.CreateAsync(user, User_Password);
        }

        await context.SaveChangesAsync();
    }
}