namespace Persistence.DataTransferObject;

public class UserDTO
{
    public Guid Id { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public Guid AvatarId { get; set; }

    public string? Token { get; set; }

    public Guid? ActiveGameSessionId { get; set; }

    public GameSessionDTO? ActiveGameSession { get; set; }
}