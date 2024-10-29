using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ScheduleApp.Models; // Make sure this is the correct namespace for your Student model
using System.Linq;

namespace ScheduleApp.Controllers
{
    public class StudentController : Controller
    {
        // Class-level variable
        private static List<Student> students = new List<Student>
        {
            new Student { StudentId = 1, FirstName = "Maureen", LastName = "Sakala", Email = "sakalam336@gmail.com", StudentNumber = "bit2022167", Password = "well", Program = "ict"},
            new Student { StudentId = 2, FirstName = "Mbuzi", LastName = "Mwale", Email = "mm456@.com", StudentNumber = "bba2000034", Password = "well", Program = "business administration"}
        };

        // GET: /Student
        public IActionResult Index()
        {
            return View(students);
        }

        // GET: /Student/Details/1
        public IActionResult Details(int id)
        {
            var student = students.FirstOrDefault(s => s.StudentId == id);
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
                // Assign a new ID (In a real application, this would come from the database)
                student.StudentId = students.Max(s => s.StudentId) + 1; // Fixed to use StudentId
                students.Add(student);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: /Student/Edit/1
        public IActionResult Edit(int id)
        {
            var student = students.FirstOrDefault(s => s.StudentId == id);
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
                var existingStudent = students.FirstOrDefault(s => s.StudentId == student.StudentId);
                if (existingStudent != null)
                {
                    existingStudent.FirstName = student.FirstName;
                    existingStudent.LastName = student.LastName; // Updated to include LastName
                    existingStudent.Email = student.Email;
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }
            return View(student);
        }

        // GET: /Student/Delete/1
        public IActionResult Delete(int id)
        {
            var student = students.FirstOrDefault(s => s.StudentId == id);
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
            var student = students.FirstOrDefault(s => s.StudentId == id);
            if (student != null)
            {
                students.Remove(student);
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        // GET: /Student/Signup
        public IActionResult Signup()
        {
            return View();
        }

        // POST: /Student/Signup
        [HttpPost]
        public IActionResult Signup(StudentSignupModel model)
        {
            if (ModelState.IsValid)
            {
                // Create a new student object
                var newStudent = new Student
                {
                    StudentId = students.Max(s => s.StudentId) + 1, // Assign a new ID
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    StudentNumber = model.StudentNumber,
                    Program = model.Program,
                    Password = model.Password // In a real app, you should hash this
                };

                // Add the new student to the list
                students.Add(newStudent);

                // Redirect to the Index or login page
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
    }
}


