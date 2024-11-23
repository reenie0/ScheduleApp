using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ScheduleApp.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        // Student Dashboard
        [Authorize(Policy = "StudentOnly")]
        public IActionResult StudentDashboard()
        {
            return View(); // View for Student Dashboard
        }

        // Lecturer Dashboard
        [Authorize(Policy = "LecturerOnly")]
        public IActionResult LecturerDashboard()
        {
            return View(); // View for Lecturer Dashboard
        }
    }
}

