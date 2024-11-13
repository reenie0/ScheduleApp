using Microsoft.AspNetCore.Identity;

namespace ScheduleApp.Models;

public class Student : IdentityUser
{
    public Student()
    {
    }

    public Student(string userName) : base(userName)
    {
    }

    public int StudentId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required  new string Email { get; set; }
    public required string StudentNumber { get; set; }
    public required string Program { get; set; }
    public required string HashedPassword { get; set; }
    public  required string Password { get; set; }
    

    public ICollection<Request>? Requests { get; set; }
}
