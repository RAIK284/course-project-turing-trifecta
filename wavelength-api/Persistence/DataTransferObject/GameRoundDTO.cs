using Domain;

namespace Persistence.DataTransferObject;

public class GameRoundDTO
{
    public Guid Id { get; set; }

    public Guid GameSessionId { get; set; }

    /// <summary>
    ///     Gets or sets the value that determines the team who's turn it is this round.
    /// </summary>
    public Team TeamTurn { get; set; }

    public Guid SpectrumCardId { get; set; }

    public SpectrumCard spectrumCard { get; set; }

    /// <summary>
    ///     Gets or sets the clue given by the psychic.
    /// </summary>
    public string Clue { get; set; }

    public int TargetOffset { get; set; }
    
    public List<GameSessionMemberRoundRoleDTO> RoundRoles { get; set; }
    
    public List<GameRoundGhostGuessDTO> GhostGuesses { get; set; }
    
    public GameRoundSelectorSelectionDTO SelectorSelection { get; set; }
    
    public List<GameRoundOpposingTeamGuessDTO> OpposingGhostGuesses { get; set; }
    
    public GameRoundOpposingTeamSelectionDTO OpposingTeamSelection { get; set; }
}