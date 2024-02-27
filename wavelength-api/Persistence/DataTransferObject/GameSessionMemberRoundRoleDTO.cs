using Domain;

namespace Persistence.DataTransferObject;

public class GameSessionMemberRoundRoleDTO
{
    public Guid ID { get; set; }
    
    public Guid UserID { get; set; }
    
    public Guid GameSessionID { get; set; }
    
    public GameSession? GameSession { get; set; }
    
    public Guid GameRoundID { get; set; }
    
    public GameRoundDTO? GameRound { get; set; }
    
    public TeamRole Role { get; set; }
    
    public Team Team { get; set; }
}