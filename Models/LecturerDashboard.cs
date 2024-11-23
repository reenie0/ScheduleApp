using ScheduleApp.ViewModels;
using System.ComponentModel.DataAnnotations;
using ScheduleApp.Models;
using Microsoft.EntityFrameworkCore.Query;

namespace ScheduleApp.ViewModels
{
    public class LecturerDashboard
    {
        
        public required Lecturer Lecturer { get; set; }
        
       
       
        // Navigation property for any related entities, e.g., schedules or courses
        public List<Schedule>? Schedules { get; set; }
        public List<AppointmentRequest>? AppointmentRequest { get; set; }
        
    }
}
