
namespace ScheduleApp.Models;

public class LecturerSchedule
{
    public int Id { get; set; }
    public  required int LecturerId { get; set; }
    public required  string LecturerName { get; set; }
    public DateTime Date { get; set; }
    public IEnumerable<Schedule>? Schedules { get; set; }
}
