// Models/StudentSignupModel.cs
using System.ComponentModel.DataAnnotations;

namespace ScheduleApp.Models
{
    public class StudentSignupModel
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string StudentNumber { get; set; }
        public required string Program { get; set; }
        public required string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public required string ConfirmPassword { get; set; }
    }
    
    }

