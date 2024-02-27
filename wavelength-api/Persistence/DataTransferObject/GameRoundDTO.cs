using Domain;

namespace Persistence.DataTransferObject;

public class GameRoundDTO
{
    public Guid ID { get; set; }

    public Guid GameSessionID { get; set; }

    /// <summary>
    ///     Gets or sets the value that determines the team who's turn it is this round.
    /// </summary>
    public Team TeamTurn { get; set; }

    public Guid SpectrumCardID { get; set; }

    public SpectrumCard spectrumCard { get; set; }

    /// <summary>
    ///     Gets or sets the clue given by the psychic.
    /// </summary>
    public string Clue { get; set; }

    public int TargetOffset { get; set; }
}