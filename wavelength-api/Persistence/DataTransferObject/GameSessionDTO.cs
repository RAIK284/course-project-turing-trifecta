namespace Persistence.DataTransferObject;

public class GameSessionDTO
{
    public Guid Id { get; set; }

    public string JoinCode { get; set; }

    public Guid OwnerId { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public List<GameSessionMemberDTO>? Members { get; set; }

    public List<GameRoundDTO> Rounds { get; set; } = new();

    public int GameRound { get; set; }
}