namespace Persistence.DataTransferObject;

/// <summary>
///     Represents the two ends of the spectrum used in a round.
///     (e.g. Underrated -- Overrated)
/// </summary>
public class SpectrumCardDTO
{
    public Guid Id { get; set; }

    public string LeftName { get; set; }

    public string RightName { get; set; }
}