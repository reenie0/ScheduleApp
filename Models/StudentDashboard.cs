using ScheduleApp.Models;

namespace ScheduleApp.ViewModels
{
    public class StudentDashboard
    {
        
        public required string StudentName { get; set; }
        public List<LecturerSchedule>? LectureSchedules { get; set; } = new List<LecturerSchedule>();
        public List<Notification>? Notifications { get; set; } 
        public List<AvailableSlot>? AvailableSlots { get; set; } 
        public List<Schedule>? Schedules { get; set; }
    }

  

   
}

