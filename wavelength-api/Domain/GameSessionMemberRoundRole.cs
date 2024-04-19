namespace Domain;

public class GameSessionMemberRoundRole
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    
    public Guid GameSessionId { get; set; }
    
    public Guid GameRoundId { get; set; }
    
    public TeamRole Role { get; set; }
    
    public Team Team { get; set; }
}