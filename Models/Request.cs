namespace ScheduleApp.Models
{
    public class Request
    {
        public int RequestId { get; set; }
        public int StudentId { get; set; }
        public int LecturerId { get; set; }
        public int ScheduleId { get; set; }
        public DateTime RequestTime { get; set; }
        public required string Status { get; set; }


        public required Student student { get; set; }
        public required Lecturer Lecturer { get; set; }
        public required virtual Schedule Schedule { get; set; }


    }
}
