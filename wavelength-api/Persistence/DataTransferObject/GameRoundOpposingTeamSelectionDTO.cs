using Domain;

namespace Persistence.DataTransferObject;

public class GameRoundOpposingTeamSelectionDTO
{
    public Guid ID { get; set; }

    public Guid GameSessionID { get; set; }

    public Guid GameRoundID { get; set; }

    /// <summary>
    ///     Gets or sets the value that represents whether this team chose left or right of the opposing team's guess.
    /// </summary>
    public bool IsLeft { get; set; }

    public Team Team { get; set; }
}