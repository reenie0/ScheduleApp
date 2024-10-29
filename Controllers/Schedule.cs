using Microsoft.AspNetCore.Mvc;
using ScheduleApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace ScheduleApp.Controllers
{
    public class ScheduleController : Controller
    {
        private static List<Schedule> schedules = new List<Schedule>
        {
            new Schedule { ScheduleId = 1, LecturerId = 1 },
            new Schedule { ScheduleId = 2, LecturerId = 2 }
        };

        // GET: /Schedule
        public IActionResult Index()
        {
            return View(schedules);
        }

        // GET: /Schedule/Details/1
        public IActionResult Details(int id)
        {
            var schedule = schedules.FirstOrDefault(s => s.ScheduleId == id);
            if (schedule == null) return NotFound();
            return View(schedule);
        }

        // GET: /Schedule/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Schedule/Create
        [HttpPost]
        public IActionResult Create(Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                schedule.ScheduleId = schedules.Max(s => s.ScheduleId) + 1;
                schedules.Add(schedule);
                return RedirectToAction(nameof(Index));
            }
            return View(schedule);
        }

        // GET: /Schedule/Edit/1
        public IActionResult Edit(int id)
        {
            var schedule = schedules.FirstOrDefault(s => s.ScheduleId == id);
            if (schedule == null) return NotFound();
            return View(schedule);
        }

        // POST: /Schedule/Edit/1
        [HttpPost]
        public IActionResult Edit(Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                var existingSchedule = schedules.FirstOrDefault(s => s.ScheduleId == schedule.ScheduleId);
                if (existingSchedule != null)
                {
                    existingSchedule.AvailableSlots = schedule.AvailableSlots;
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }
            return View(schedule);
        }

        // GET: /Schedule/Delete/1
        public IActionResult Delete(int id)
        {
            var schedule = schedules.FirstOrDefault(s => s.ScheduleId == id);
            if (schedule == null) return NotFound();
            return View(schedule);
        }

        // POST: /Schedule/Delete/1
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var schedule = schedules.FirstOrDefault(s => s.ScheduleId == id);
            if (schedule != null)
            {
                schedules.Remove(schedule);
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
    }
}
