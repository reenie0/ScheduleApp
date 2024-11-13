using Microsoft.AspNetCore.Identity;

namespace ScheduleApp.Models
{
    public class Users : IdentityUser
    {
        
        
        public required string Password { get; set; }
        public required string Role { get; set; }
        public required string UserType { get; set; }
    }
}
