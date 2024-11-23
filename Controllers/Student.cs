using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ScheduleApp.Data;
using ScheduleApp.Models;
using System.Linq;
using System.Data;
using ScheduleApp.ViewModels;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace ScheduleApp.Controllers;

public class StudentController : Controller
{
    private readonly UserManager<Student> _userManager;
    private readonly AppDbContext _context;

    public StudentController(UserManager<Student> userManager, AppDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    // Method to get the current logged-in student's ID
    private async Task<Student> GetCurrentStudentAsync()
    {
        // Get the current user
        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            return await _context.Students.FirstOrDefaultAsync(s => s.StudentId == user.StudentId);
        }

        throw new UnauthorizedAccessException("User is not logged in.");
    }

    // GET: /Student
    public IActionResult Index()
    {
        var students = _context.Students.ToList(); 
        return View(students);
    }

    // GET: /Student/Details/1
    public IActionResult Details(int id)
    {
        var student = _context.Students.Find(id);
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
    }

    // GET: /Student/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Student/Create
    [HttpPost]
    public IActionResult Create(Student student)
    {
        if (ModelState.IsValid)
        {
            _context.Students.Add(student); 
            _context.SaveChanges(); 
            return RedirectToAction(nameof(Index));
        }
        return View(student);
    }

    // GET: /Student/Edit/1
    public IActionResult Edit(int id)
    {
        var student = _context.Students.Find(id);
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
    }

    // POST: /Student/Edit/1
    [HttpPost]
    public IActionResult Edit(Student student)
    {
        if (ModelState.IsValid)
        {
            _context.Students.Update(student); 
            _context.SaveChanges(); 
            return RedirectToAction(nameof(Index));
        }
        return View(student);
    }

    // GET: /Student/Delete/1
    public IActionResult Delete(int id)
    {
        var student = _context.Students.Find(id);
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
    }

    // POST: /Student/Delete/1
    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var student = _context.Students.Find(id);
        if (student != null)
        {
            _context.Students.Remove(student); // Remove student from the context
            _context.SaveChanges(); // Save changes to the database
            return RedirectToAction(nameof(Index));
        }
        return NotFound();
    }

    // GET: /Student/Signup
    public IActionResult Signup()
    {
        return View();
    }

    public AppDbContext Get_context()
    {
        return _context;
    }

    [HttpPost]
    public IActionResult Signup(StudentSignupModel model, AppDbContext _context)
    {
        if (ModelState.IsValid)
        {
            var passwordHasher = new PasswordHasher<Student>();
            
            var newStudent = new Student
            {

                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                StudentNumber = model.StudentNumber,
                Program = model.Program,
                HashedPassword = string.Empty,
                Password = model.Password,

            };
            newStudent.HashedPassword = passwordHasher.HashPassword(newStudent, model.Password);
            _context.Students.Add(newStudent); 
            _context.SaveChanges(); 
            return RedirectToAction(nameof(Index));
        }

        return View(model);

    }
        public IActionResult LecturerSchedules(int lecturerId)
        {
            var lecturer = _context.Lecturers.Find(lecturerId);
            if (lecturer == null) return NotFound();

            var schedules = _context.Schedules
                .Where(s => s.LecturerId == lecturerId)
                .OrderBy(s => s.Date)
                .ThenBy(s => s.StartTime)
                .ToList();

            var viewModel = new LecturerSchedule
            {
                LecturerId = lecturerId,
                LecturerName = $"{lecturer.FirstName} {lecturer.LastName}",
                Schedules = schedules

            };

            return View(viewModel);
        }
    [HttpPost]
    public async Task<IActionResult> RequestAppointment(int scheduleId)
    {
        
        var student = await GetCurrentStudentAsync();
        if (student == null)
        {
            return Unauthorized(); 
        }

    
        var schedule = _context.Schedules.Find(scheduleId);
        if (schedule == null || !schedule.IsAvailable)
        {
            return NotFound(); 
        }

       
        var appointment = new AppointmentRequest
        {
            StudentId = student.StudentId,   
            Student = student,              
            ScheduleId = scheduleId,         
            Schedule = schedule,             
            IsApproved = false,             
            RequestDate = DateTime.Now,
            Status = string.Empty,
        };

       
        _context.AppointmentRequests.Add(appointment);
        _context.SaveChanges();

        // Mark the schedule as unavailable if needed
        schedule.IsAvailable = false;
        _context.SaveChanges();

        return RedirectToAction("StudentDashboard"); // Redirect after saving
    }



    [HttpGet]
    [HttpPost]
    public async Task<IActionResult> StudentDashboard()
    {
        var currentStudent = await GetCurrentStudentAsync(); 
        var ViewModel = new StudentDashboard
        {
            StudentName = currentStudent.FirstName + " " + currentStudent.LastName,
            LectureSchedules = await _context.Schedules
                .Where(s => s.StudentId == currentStudent.StudentId)
                .Select(s => new LecturerSchedule
                {
                    LecturerId = s.LecturerId,
                    Date = s.Date,
                    LecturerName = s.LecturerId.ToString(),
                }).ToListAsync(),
            Notifications = await _context.Notifications
                .Where(n => n.UserId == currentStudent.StudentId && n.UserType == "Student")
                .ToListAsync(),
            AvailableSlots = await _context.AvailableSlots
                .Where(a => a.StudentId == currentStudent.Id)
                .ToListAsync()
        };

        return View(ViewModel);
    }
}


   








