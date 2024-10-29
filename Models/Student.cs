namespace ScheduleApp.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string StudentNumber { get; set; }
        public required string Program { get; set; }

        public ICollection<Request>? Requests { get; set; }
    }
}
