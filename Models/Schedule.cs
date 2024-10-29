namespace ScheduleApp.Models
{
    public class Schedule
    {
        internal int AvailableSlots;

        public int ScheduleId { get; set; }
        public int LecturerId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool isAvailable { get; set; }

        public virtual ICollection<Request>? Request { get; set; }
    }
}
