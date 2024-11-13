using ScheduleApp.Models;

public class Request
{
    public int RequestId { get; set; }

    // Define the foreign key properties explicitly
    public required string  StudentId { get; set; }
    public required String  LecturerId { get; set; }
    public int ScheduleId { get; set; }
    public DateTime RequestTime { get; set; }
    public required string Status { get; set; }

    // Define the navigation properties (no need to define StudentId here again)
    public required Student Student { get; set; } // Navigation property
    public required Lecturer Lecturer { get; set; } // Navigation property
    public required Schedule Schedule { get; set; } // Navigation property
}
