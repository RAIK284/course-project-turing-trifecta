namespace Domain;

/// <summary>
///     Represents the two ends of the spectrum used in a round.
///     (e.g. Underrated -- Overrated)
/// </summary>
public class SpectrumCard
{
    public Guid ID { get; set; }

    public string LeftName { get; set; }

    public string RightName { get; set; }
}