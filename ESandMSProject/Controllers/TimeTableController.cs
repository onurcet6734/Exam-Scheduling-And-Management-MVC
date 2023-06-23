using ESandMSProject.Models;
using ESandMSProject.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ESandMSProject.Controllers
{
    public class TimeTableController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TimeTableController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Exams = _context.Exams.ToList();
            ViewBag.Halls = _context.Halls.ToList();
            ViewBag.Students = _context.Students.ToList();

            var data = Request.Cookies["loginInfos"];
            if (string.IsNullOrEmpty(data)) // Check if cookie data is null or empty
            {
                return RedirectToAction("Index", "Logins"); // Redirect to login page
            }
            Login login = JsonConvert.DeserializeObject<Login>(data);
            List<Scheduling> printedInfos = await _context.Schedulings
                .Include(x => x.Student)
                .ThenInclude(x => x.Login)
                .Where(x => x.Student.LoginId == login.Id).ToListAsync();

            return View(printedInfos);
        }
    }
}