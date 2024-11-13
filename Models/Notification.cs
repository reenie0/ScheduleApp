namespace ScheduleApp.Models;

public class Notification
{
    public int NotificationId { get; set; }
    public int UserId { get; set; }
    public required string UserType { get; set; }
    public required string Message { get; set; }
    public DateTime Timestamp { get; set; }
    public bool IsRead { get; set; }
    public required  string RecipientEmail { get; set; }  
   


    public required virtual Student Student { get; set; }
    public required virtual Lecturer Lecturer { get; set; }

}
