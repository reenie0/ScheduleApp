using Microsoft.AspNetCore.Mvc;
using ScheduleApp.Models; // Ensure this is the correct namespace for your Lecturer model
using System.Collections.Generic;
using System.Linq;

namespace ScheduleApp.Controllers
{
    public class LecturerController : Controller
    {
        // Class-level variable
        private static List<Lecturer> lecturers = new List<Lecturer>
        {
            // Initial list can be populated here if needed
        };

        // GET: /Lecturer
        public IActionResult Index()
        {
            return View(lecturers);
        }

        // GET: /Lecturer/Details/1
        public IActionResult Details(int id)
        {
            var lecturer = lecturers.FirstOrDefault(l => l.LecturerId == id);
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
                lecturer.LecturerId = lecturers.Max(l => l.LecturerId) + 1;
                lecturers.Add(lecturer);
                return RedirectToAction(nameof(Index));
            }
            return View(lecturer);
        }

        // GET: /Lecturer/Edit/1
        public IActionResult Edit(int id)
        {
            var lecturer = lecturers.FirstOrDefault(l => l.LecturerId == id);
            if (lecturer == null) return NotFound();
            return View(lecturer);
        }

        // POST: /Lecturer/Edit/1
        [HttpPost]
        public IActionResult Edit(Lecturer lecturer)
        {
            if (ModelState.IsValid)
            {
                var existingLecturer = lecturers.FirstOrDefault(l => l.LecturerId == lecturer.LecturerId);
                if (existingLecturer != null)
                {
                    existingLecturer.FirstName = lecturer.FirstName;
                    existingLecturer.LastName = lecturer.LastName; // Include LastName
                    existingLecturer.Email = lecturer.Email;
                    existingLecturer.LecturerNumber = lecturer.LecturerNumber; // Include LecturerNumber
                    existingLecturer.Department = lecturer.Department; // Include Department
                    existingLecturer.Title = lecturer.Title; // Include Title
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }
            return View(lecturer);
        }

        // GET: /Lecturer/Delete/1
        public IActionResult Delete(int id)
        {
            var lecturer = lecturers.FirstOrDefault(l => l.LecturerId == id);
            if (lecturer == null) return NotFound();
            return View(lecturer);
        }

        // POST: /Lecturer/Delete/1
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var lecturer = lecturers.FirstOrDefault(l => l.LecturerId == id);
            if (lecturer != null)
            {
                lecturers.Remove(lecturer);
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
            if (ModelState.IsValid)
            {
                // Create a new lecturer object
                Lecturer newLecturer = new Lecturer
                {
                    LecturerId = lecturers.Max(l => l.LecturerId) + 1, // Assign a new ID
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    LecturerNumber = model.LecturerNumber,
                    Department = model.Department,
                    Title = model.Title,
                    Phone = model. Phone,
                    Password = model.Password // In a real app, you should hash this
                };

                // Add the new lecturer to the list
                lecturers.Add(newLecturer);

                // Redirect to the Index or login page
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
    }
}
