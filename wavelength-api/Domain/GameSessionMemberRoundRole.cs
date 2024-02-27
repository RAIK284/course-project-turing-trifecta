namespace Domain;

public class GameSessionMemberRoundRole
{
    public Guid ID { get; set; }
    
    public Guid UserID { get; set; }
    
    public Guid GameSessionID { get; set; }
    
    public Guid GameRoundID { get; set; }
    
    public TeamRole Role { get; set; }
    
    public Team Team { get; set; }
}