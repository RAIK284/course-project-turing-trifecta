namespace Domain;

public class GameSession
{
    public Guid ID { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string JoinCode { get; set; }

    public Guid OwnerID { get; set; }
    
    /// <summary>
    /// Gets or sets the object that holds the score for each team for this game.
    /// </summary>
    public GameSessionResult GameSessionResult { get; set; }

    public ICollection<GameSessionMember> Members { get; set; } = new List<GameSessionMember>();
}