using Domain;

namespace Persistence.DataTransferObject;

public class GameSessionMemberDTO
{
    public Guid Id { get; set; }

    public Guid GameSessionId { get; set; }

    public Guid UserId { get; set; }

    public User? User { get; set; }

    public Team Team { get; set; }
}