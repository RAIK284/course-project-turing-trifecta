using Domain;

namespace Persistence.DataTransferObject;

public class GameSessionResultDTO
{
    public Guid ID { get; set; }

    public Guid GameSessionID { get; set; }

    public Team WinningTeam { get; set; }

    public int WinningScore { get; set; }

    public Team LosingTeam { get; set; }

    public int LosingScore { get; set; }
}