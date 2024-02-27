namespace Persistence.DataTransferObject;

public class UserDTO
{
    public Guid ID { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public Guid AvatarID { get; set; }

    public string? Token { get; set; }

    public Guid? GameSessionID { get; set; }

    public GameSessionDTO? GameSession { get; set; }

    public Guid? GameSessionMemberID { get; set; }

    public GameSessionMemberDTO? GameSessionMember { get; set; }
}