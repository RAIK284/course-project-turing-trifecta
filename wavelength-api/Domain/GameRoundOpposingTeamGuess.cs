namespace Domain;

public class GameRoundOpposingTeamGuess
{
    public Guid Id { get; set; }

    public Guid GameSessionId { get; set; }

    public Guid GameRoundId { get; set; }
    
    public GameRound GameRound { get; set; }

    /// <summary>
    ///     Gets or sets the value that represents whether this team chose left or right of the opposing team's guess.
    /// </summary>
    public bool IsLeft { get; set; }

    public Guid UserId { get; set; }
    
    public Team Team { get; set; }
}