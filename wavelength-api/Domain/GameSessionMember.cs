using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class GameSessionMember
{
    public string UserID { get; set; }

    [ForeignKey("UserID")] public User User { get; set; }

    public Guid GameSessionID { get; set; }

    [ForeignKey("GameSessionID")] public GameSession GameSession { get; set; }

    public Team Team { get; set; } = 0;
}