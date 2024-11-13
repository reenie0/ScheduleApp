using System.Reflection;

namespace ScheduleApp.Models;

public class Schedule
{
    internal int AvailableSlots;
    

    public int ScheduleId { get; set; }
    public int LecturerId { get; set; }
    public int StudentId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public DateTime Date {  get; set; }
    public required string Title { get; set; }
    public required string Location { get; set; }
    public bool IsAvailable { get;internal set; }
    public virtual ICollection<Request>? Request { get; set; }
}
