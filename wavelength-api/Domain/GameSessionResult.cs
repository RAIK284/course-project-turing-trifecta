namespace Domain;

public class GameSessionResult
{
    public Guid Id { get; set; }

    public Guid GameSessionId { get; set; }
    
    public GameSession GameSession { get; set; }

    public Team WinningTeam { get; set; }

    public int WinningScore { get; set; }

    public Team LosingTeam { get; set; }

    public int LosingScore { get; set; }
}