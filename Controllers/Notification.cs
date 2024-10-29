using Microsoft.AspNetCore.Mvc;
using ScheduleApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace ScheduleApp.Controllers
{
    public class NotificationController : Controller
    {
        // Class-level variable to store notifications
        private static List<Notification> notifications = new List<Notification>
        {
            new Notification
            {
                NotificationId = 1,
                UserId = 1,
                UserType = "Student",
                Message = "Your meeting request was accepted.",
                Timestamp = DateTime.Now,
                IsRead = false,
                RecipientEmail = "student@example.com",
                Student = new Student { StudentId = 1, FirstName = "Maureen", LastName = "Sakala", Email = "student@example.com", StudentNumber = "bit2022167", Password="well", Program="ict" },
                Lecturer = null
            }
        };

        // GET: /Notification
        public IActionResult Index()
        {
            return View(notifications);
        }

        // GET: /Notification/Details/1
        public IActionResult Details(int id)
        {
            var notification = notifications.FirstOrDefault(n => n.NotificationId == id);
            if (notification == null)
            {
                return NotFound();
            }
            return View(notification);
        }

        // GET: /Notification/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Notification/Create
        [HttpPost]
        public IActionResult Create(Notification notification)
        {
            if (ModelState.IsValid)
            {
                notification.NotificationId = notifications.Max(n => n.NotificationId) + 1;
                notifications.Add(notification);
                return RedirectToAction(nameof(Index));
            }
            return View(notification);
        }

        // GET: /Notification/Edit/1
        public IActionResult Edit(int id)
        {
            var notification = notifications.FirstOrDefault(n => n.NotificationId == id);
            if (notification == null)
            {
                return NotFound();
            }
            return View(notification);
        }

        // POST: /Notification/Edit/1
        [HttpPost]
        public IActionResult Edit(Notification notification)
        {
            if (ModelState.IsValid)
            {
                var existingNotification = notifications.FirstOrDefault(n => n.NotificationId == notification.NotificationId);
                if (existingNotification != null)
                {
                    existingNotification.UserId = notification.UserId;
                    existingNotification.UserType = notification.UserType;
                    existingNotification.Message = notification.Message;
                    existingNotification.Timestamp = notification.Timestamp;
                    existingNotification.IsRead = notification.IsRead;
                    existingNotification.RecipientEmail = notification.RecipientEmail;
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }
            return View(notification);
        }

        // GET: /Notification/Delete/1
        public IActionResult Delete(int id)
        {
            var notification = notifications.FirstOrDefault(n => n.NotificationId == id);
            if (notification == null)
            {
                return NotFound();
            }
            return View(notification);
        }

        // POST: /Notification/Delete/1
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var notification = notifications.FirstOrDefault(n => n.NotificationId == id);
            if (notification != null)
            {
                notifications.Remove(notification);
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
    }
}
