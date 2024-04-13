using Domain;

namespace Persistence.DataTransferObject;

public class GameSessionMemberRoundRoleDTO
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    
    public Guid GameSessionId { get; set; }
    
    public GameSession? GameSession { get; set; }
    
    public Guid GameRoundId { get; set; }
    
    public GameRoundDTO? GameRound { get; set; }
    
    public TeamRole Role { get; set; }
    
    public Team Team { get; set; }
}