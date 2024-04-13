namespace Domain;

public class GameSessionMember
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; }

    public Guid GameSessionId { get; set; }

    public GameSession GameSession { get; set; }

    public Team Team { get; set; } = 0;
}