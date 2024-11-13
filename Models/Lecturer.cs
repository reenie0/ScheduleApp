using Microsoft.AspNetCore.Identity;
using ScheduleApp.ViewModels;

namespace ScheduleApp.Models;

public class Lecturer : IdentityUser
{

    public int  LecturerId { get; set; }
    public required string LecturerNumber { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required new string Email { get; set; }
    public required string Phone {  get; set; }
    public required string Title { get; set; }
    public required string HashedPassword { get; set; }
    public required string Department { get; set; }
    



    public ICollection<Schedule>? Schedules { get; set; }
    public ICollection<Request>? Request { get; set; }
    public ICollection<AvailableSlot> AvailableSlots { get; set; }


}
