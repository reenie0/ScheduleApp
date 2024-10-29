using Microsoft.AspNetCore.Mvc;
using ScheduleApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace ScheduleApp.Controllers
{
    public class DepartmentController : Controller
    {
        // Class-level variable to store departments
        private static List<Department> departments = new List<Department>
        {
            new Department
            {
                DepartmentId = 1,
                DepartmentName = "Computer Science",
                Lecturer = new List<Lecturer>
                {
                    
                }
            },
            new Department
            {
                DepartmentId = 2,
                DepartmentName = "Business Administration",
                Lecturer = new List<Lecturer>()
            }
        };

        // GET: /Department
        public IActionResult Index()
        {
            return View(departments);
        }

        // GET: /Department/Details/1
        public IActionResult Details(int id)
        {
            var department = departments.FirstOrDefault(d => d.DepartmentId == id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // GET: /Department/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Department/Create
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                department.DepartmentId = departments.Max(d => d.DepartmentId) + 1;
                departments.Add(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: /Department/Edit/1
        public IActionResult Edit(int id)
        {
            var department = departments.FirstOrDefault(d => d.DepartmentId == id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: /Department/Edit/1
        [HttpPost]
        public IActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                var existingDepartment = departments.FirstOrDefault(d => d.DepartmentId == department.DepartmentId);
                if (existingDepartment != null)
                {
                    existingDepartment.DepartmentName = department.DepartmentName;
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }
            return View(department);
        }

        // GET: /Department/Delete/1
        public IActionResult Delete(int id)
        {
            var department = departments.FirstOrDefault(d => d.DepartmentId == id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: /Department/Delete/1
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var department = departments.FirstOrDefault(d => d.DepartmentId == id);
            if (department != null)
            {
                departments.Remove(department);
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
    }
}
