using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ScheduleApp.Data;
using ScheduleApp.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ScheduleApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly SignInManager<Users> _signInManager; // For Identity-based Students
        private readonly UserManager<Users> _userManager; // For Identity-based Students
        private readonly PasswordHasher<Lecturer> _lecturerPasswordHasher;

        public AccountController(AppDbContext context, SignInManager<Users> signInManager, UserManager<Users> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _lecturerPasswordHasher = new PasswordHasher<Lecturer>();
        }

        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Student Login
                if (model.UserType == "Student")
                {
                    var student = await _userManager.FindByEmailAsync(model.Email);
                    if (student != null)
                    {
                        var result = await _signInManager.PasswordSignInAsync(student, model.Password, model.RememberMe, lockoutOnFailure: false);
                        if (result.Succeeded)
                        {
                            // Redirect to Student Dashboard
                            return RedirectToAction("StudentDashboard", "Dashboard");
                        }
                        ModelState.AddModelError("", "Invalid student credentials.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Student not found.");
                    }
                }
                // Lecturer Login
                else if (model.UserType == "Lecturer")
                {
                    var lecturer = _context.Lecturers.FirstOrDefault(l => l.Email == model.Email);
                    if (lecturer != null)
                    {
                        var passwordVerificationResult = _lecturerPasswordHasher.VerifyHashedPassword(lecturer, lecturer.HashedPassword, model.Password);
                        if (passwordVerificationResult == PasswordVerificationResult.Success)
                        {
                            // Redirect to Lecturer Dashboard
                            return RedirectToAction("LecturerDashboard", "Dashboard");
                        }
                        ModelState.AddModelError("", "Invalid lecturer credentials.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Lecturer not found.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid user type selected.");
                }
            }

            return View(model); // Return to Login view if there is an error
        }



        // GET: /Account/LecturerSignup
        public IActionResult LecturerSignup()
        {
            return View();
        }

        // POST: /Account/LecturerSignup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LecturerSignup(LecturerSignupModel model)
        {
            if (ModelState.IsValid)
            {
                var newLecturer = new Lecturer
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    LecturerNumber = model.LecturerNumber,
                    Department = model.Department,
                    Title = model.Title,
                    Phone = model.Phone,
                    HashedPassword = _lecturerPasswordHasher.HashPassword(null, model.Password)
                   
                };

                _context.Lecturers.Add(newLecturer);
                await _context.SaveChangesAsync();

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, newLecturer.Email),
                    new Claim("UserType", "Lecturer")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Lecturer");
            }
            return View(model);
        }

        // GET: /Account/StudentSignup
        public IActionResult StudentSignup()
        {
            return View();
        }

        // POST: /Account/StudentSignup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StudentSignup(StudentSignupModel model)
        {
            if (ModelState.IsValid)
            {
                var newStudent = new Users // Use your Identity user type
                {
                   
                    Email = model.Email,
                    UserType = model.Email,
                    Password = model.Email ,
                    Role = model.Email,
                };

                var result = await _userManager.CreateAsync(newStudent, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Student");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        // GET: /Account/Index
        public IActionResult Index()
        {
            return View();
        }
    }
}


