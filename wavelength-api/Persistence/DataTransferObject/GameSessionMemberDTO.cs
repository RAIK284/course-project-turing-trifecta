using Domain;

namespace Persistence.DataTransferObject;

public class GameSessionMemberDTO
{
    public Guid ID { get; set; }

    public Guid GameSessionID { get; set; }

    public GameSessionDTO? GameSession { get; set; }

    public Guid UserID { get; set; }

    public Team Team { get; set; }

    public TeamRole TeamRole { get; set; }
}