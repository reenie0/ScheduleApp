using System;
using ScheduleApp.Models;

namespace ScheduleApp.ViewModels;

public class AppointmentRequest
{
    public int AppointmentRequestId { get; set; }
    public int StudentId { get; set; }
    public int ScheduleId { get; set; }
    public DateTime RequestDate { get; set; }
    public bool IsApproved { get; set; } 
    public int LecturerId { get; set; }
    public required string Status {  get; set; }

    // Navigation properties
    public required virtual Student Student { get; set; }
    public required virtual Schedule Schedule { get; set; }
}

