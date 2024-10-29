using Microsoft.AspNetCore.Mvc;
using ScheduleApp.Models;

namespace ScheduleApp.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Example hardcoded login for simplicity.
                // Replace this with actual authentication logic using your database.

                if (model.UserType == "Student")
                {
                    // Check student credentials (replace with DB lookup)
                    if (model.Email == "student@example.com" && model.Password == "password")
                    {
                        // Redirect to Student Dashboard or Index
                        return RedirectToAction("Index", "Student");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid student credentials");
                    }
                }
                else if (model.UserType == "Lecturer")
                {
                    // Check lecturer credentials (replace with DB lookup)
                    if (model.Email == "lecturer@example.com" && model.Password == "password")
                    {
                        // Redirect to Lecturer Dashboard or Index
                        return RedirectToAction("Index", "Lecturer");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid lecturer credentials");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid user type selected");
                }
            }
            return View(model);
        }
    }
}
