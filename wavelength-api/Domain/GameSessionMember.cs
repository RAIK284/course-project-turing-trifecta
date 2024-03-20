namespace Domain;

public class GameSessionMember
{
    public Guid ID { get; set; }

    public Guid UserID { get; set; }

    public User User { get; set; }

    public Guid GameSessionID { get; set; }

    public GameSession GameSession { get; set; }

    public Team Team { get; set; } = 0;
}