namespace Domain;

public class GameSession
{
    public Guid ID { get; set; }
    
    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }
    
    public String JoinCode { get; set; }
    
    public Guid OwnerID { get; set; }
    
}