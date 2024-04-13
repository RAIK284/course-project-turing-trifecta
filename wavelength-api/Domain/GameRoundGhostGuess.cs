namespace Domain;

public class GameRoundGhostGuess
{
    public Guid Id { get; set; }

    public Guid GameSessionId { get; set; }

    public Guid GameRoundId { get; set; }
    
    public GameRound GameRound { get; set; }

    /// <summary>
    ///     Gets or sets the degrees of offset from the origin where the team placed their guess.
    /// </summary>
    public int TargetOffset { get; set; }

    public Guid UserId { get; set; }
    
    public Team Team { get; set; }
}