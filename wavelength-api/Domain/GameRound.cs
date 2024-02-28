namespace Domain;

public class GameRound
{
    public Guid ID { get; set; }

    public Guid GameSessionID { get; set; }

    /// <summary>
    ///     Gets or sets the value that determines the team who's turn it is this round.
    /// </summary>
    public Team TeamTurn { get; set; }

    public Guid SpectrumCardID { get; set; }

    public SpectrumCard SpectrumCard { get; set; }

    /// <summary>
    ///     Gets or sets the clue given by the psychic.
    /// </summary>
    public string Clue { get; set; } = "";

    public int TargetOffset { get; set; }

    public int RoundNumber { get; set; }

    public IEnumerable<GameRoundGhostGuess> GhostGuesses { get; set; } = new List<GameRoundGhostGuess>();

    public IEnumerable<GameRoundOpposingTeamGuess> OpposingGhostGuesses { get; set; } =
        new List<GameRoundOpposingTeamGuess>();
    
    public GameRoundSelectorSelection SelectorSelection { get; set; }
    
    public GameRoundOpposingTeamSelection OpposingSelectorSelection { get; set; }

    public IEnumerable<GameSessionMemberRoundRole> RoundRoles { get; set; } = new List<GameSessionMemberRoundRole>();
}