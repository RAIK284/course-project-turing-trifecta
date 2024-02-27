using Microsoft.AspNetCore.Identity;

namespace Domain;

public class User : IdentityUser
{
    public Guid AvatarID { get; set; }

    public ICollection<GameSessionMember> GameSessions { get; set; } = new List<GameSessionMember>();
}