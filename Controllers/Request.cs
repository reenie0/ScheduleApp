using Microsoft.AspNetCore.Mvc;
using ScheduleApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace ScheduleApp.Controllers;

public class RequestController : Controller
{
    private static List<Request> requests = new List<Request>
    {
    };

    // GET: /Request
    public IActionResult Index()
    {
        return View(requests);
    }

    // GET: /Request/Details/1
    public IActionResult Details(int id)
    {
        var request = requests.FirstOrDefault(r => r.RequestId == id);
        if (request == null) return NotFound();
        return View(request);
    }

    // GET: /Request/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Request/Create
    [HttpPost]
    public IActionResult Create(Request request)
    {
        if (ModelState.IsValid)
        {
            request.RequestId = requests.Max(r => r.RequestId) + 1;
            requests.Add(request);
            return RedirectToAction(nameof(Index));
        }
        return View(request);
    }

    // GET: /Request/Edit/1
    public IActionResult Edit(int id)
    {
        var request = requests.FirstOrDefault(r => r.RequestId == id);
        if (request == null) return NotFound();
        return View(request);
    }

    // POST: /Request/Edit/1
    [HttpPost]
    public IActionResult Edit(Request request)
    {
        if (ModelState.IsValid)
        {
            var existingRequest = requests.FirstOrDefault(r => r.RequestId == request.RequestId);
            if (existingRequest != null)
            {
                existingRequest.Status = request.Status;
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
        return View(request);
    }

    // GET: /Request/Delete/1
    public IActionResult Delete(int id)
    {
        var request = requests.FirstOrDefault(r => r.RequestId == id);
        if (request == null) return NotFound();
        return View(request);
    }

    // POST: /Request/Delete/1
    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var request = requests.FirstOrDefault(r => r.RequestId == id);
        if (request != null)
        {
            requests.Remove(request);
            return RedirectToAction(nameof(Index));
        }
        return NotFound();
    }
}

