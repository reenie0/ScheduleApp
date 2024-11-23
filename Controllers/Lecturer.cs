using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScheduleApp.Data;
using ScheduleApp.Models;
using ScheduleApp.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;


namespace ScheduleApp.Controllers;

public class LecturerController : Controller
{
    private readonly AppDbContext _context; 

    public LecturerController(AppDbContext context)
    {
        _context = context; 
    }

    // GET: /Lecturer
    public IActionResult Index()
    {
        var lecturers = _context.Lecturers.ToList(); // Fetch lecturers from the database
        return View(lecturers);
    }

    // GET: /Lecturer/Details/1
    public IActionResult Details(int id)
    {
        var lecturer = _context.Lecturers.FirstOrDefault(l => l.LecturerId == id);
        if (lecturer == null) return NotFound();
        return View(lecturer);
    }

    // GET: /Lecturer/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Lecturer/Create
    [HttpPost]
    public IActionResult Create(Lecturer lecturer)
    {
        if (ModelState.IsValid)
        {
            _context.Lecturers.Add(lecturer); // Add the lecturer to the context
            _context.SaveChanges(); // Save changes to the database
            return RedirectToAction(nameof(Index));
        }
        return View(lecturer);
    }

    // GET: /Lecturer/Edit/1
    public IActionResult Edit(int id)
    {
        var lecturer = _context.Lecturers.FirstOrDefault(l => l.LecturerId == id);
        if (lecturer == null) return NotFound();
        return View(lecturer);
    }

    // POST: /Lecturer/Edit/1
    [HttpPost]
    public IActionResult Edit(Lecturer lecturer)
    {
        if (ModelState.IsValid)
        {
            _context.Lecturers.Update(lecturer); // Update the lecturer
            _context.SaveChanges(); // Save changes to the database
            return RedirectToAction(nameof(Index));
        }
        return View(lecturer);
    }

    // GET: /Lecturer/Delete/1
    public IActionResult Delete(int id)
    {
        var lecturer = _context.Lecturers.FirstOrDefault(l => l.LecturerId == id);
        if (lecturer == null) return NotFound();
        return View(lecturer);
    }

    // POST: /Lecturer/Delete/1
    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var lecturer = _context.Lecturers.FirstOrDefault(l => l.LecturerId == id);
        if (lecturer != null)
        {
            _context.Lecturers.Remove(lecturer); // Remove the lecturer
            _context.SaveChanges(); // Save changes to the database
            return RedirectToAction(nameof(Index));
        }
        return NotFound();
    }

    // GET: /Lecturer/Signup
    public IActionResult Signup()
    {
        return View();
    }

    // POST: /Lecturer/Signup
    [HttpPost]
    public IActionResult Signup(LecturerSignupModel model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        if (ModelState.IsValid)
        {
            var passwordHasher = new PasswordHasher<Lecturer>();
            // Create a new lecturer object
            Lecturer newLecturer = new Lecturer
            {
                // The ID will be generated automatically by the database
                
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                LecturerNumber = model.LecturerNumber,
                Department = model.Department,
                Title = model.Title,
                Phone = model.Phone,
                HashedPassword = string.Empty
            };
            newLecturer.HashedPassword = passwordHasher.HashPassword(newLecturer, model.Password);

            // Add the new lecturer to the context
            _context.Lecturers.Add(newLecturer);
            _context.SaveChanges(); // Save changes to the database

            // Redirect to the Index or login page
            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }

    public async Task<IActionResult> LecturerDashboard()
    {
        // Retrieve the currently logged-in lecturer’s email
        var lecturerEmail = User.Identity.Name;

        // Fetch lecturer information from the database
        var lecturer = await _context.Lecturers
            .Where(l => l.Email == lecturerEmail)
            .FirstOrDefaultAsync();

        if (lecturer == null)
        {
            return NotFound("Lecturer not found.");
        }
        

        // Fetch lecturer's schedules and pending student requests, etc.
        var schedule = await _context.Schedules
            .Where(s => s.LecturerId == lecturer.LecturerId)
            .ToListAsync();

        var requests = await _context.AppointmentRequests
            .Where(r => r.LecturerId == lecturer.LecturerId && r.Status == "Pending")
            .ToListAsync();

        // Create a view model to pass data to the view
        var viewModel = new LecturerDashboard
        {
            
             Lecturer = lecturer,
            Schedules = schedule,
            AppointmentRequest = requests
         
          
        };

        return View(viewModel);
    }

    // Other actions such as accepting or declining requests, updating availability
    // e.g., Accept Appointment Request
    public async Task<IActionResult> AcceptRequest(int requestId)
    {
        var request = await _context.AppointmentRequests.FindAsync(requestId);
        if (request == null)
            return NotFound();

        request.Status = "Accepted";
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    // Decline an appointment request
    public async Task<IActionResult> DeclineRequest(int requestId)
    {
        var request = await _context.AppointmentRequests.FindAsync(requestId);
        if (request == null)
            return NotFound();

        request.Status = "Declined";
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

}

