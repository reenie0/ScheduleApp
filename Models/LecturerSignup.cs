
using System.ComponentModel.DataAnnotations;
using ScheduleApp.Models;

namespace ScheduleApp.Models;

public class LecturerSignupModel
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string LecturerNumber { get; set; }
    public required string Department { get; set; }
    public required string Title { get; set; }
    public required  string Password { get; set; }
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public required string ConfirmPassword { get; set; }
    public required string  Phone { get; set; }
    public required bool RememberMe { get; set; }
}

