
namespace ScheduleApp.Models
{
    public class Lecturer
    {
        public int LecturerId { get; set; }
        public required string LecturerNumber { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string Title { get; set; }
        public required string Password { get; set; }
        public required string Department { get; set; }



        public ICollection<Schedule>? Schedules { get; set; }
        public ICollection<Request>? Request { get; set; }

        
        
    }
}
