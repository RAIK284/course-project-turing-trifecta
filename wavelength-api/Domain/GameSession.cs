namespace Domain;

public class GameSession
{
    public Guid ID { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string JoinCode { get; set; }

    public Guid OwnerID { get; set; }

    public ICollection<GameSessionMember> Members { get; set; }
}