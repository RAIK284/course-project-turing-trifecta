using Microsoft.AspNetCore.Identity;

namespace Domain;

public class User : IdentityUser
{
    public Guid UserId { get; set; }
    
    public Guid AvatarId { get; set; }
}