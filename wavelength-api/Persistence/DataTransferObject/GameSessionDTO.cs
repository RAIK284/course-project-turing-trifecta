using Domain;

namespace Persistence.DataTransferObject;

public class GameSessionDTO
{
    public Guid ID { get; set; }

    public string JoinCode { get; set; }

    public Guid OwnerID { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public List<GameSessionMember>? Members { get; set; }
}