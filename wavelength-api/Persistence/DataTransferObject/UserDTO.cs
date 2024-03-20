namespace Persistence.DataTransferObject;

public class UserDTO
{
    public Guid ID { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public Guid AvatarID { get; set; }

    public string? Token { get; set; }

    public Guid? ActiveGameSessionID { get; set; }

    public GameSessionDTO? ActiveGameSession { get; set; }
}