using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ESandMSProject.Models;
using ESandMSProject.Models.Domain;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using ESandMSProject.ViewModels;

namespace ESandMSProject.Controllers
{
    public class SchedulingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SchedulingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "A")]

        // GET: Schedulings
        [Authorize]
        public async Task<IActionResult> Index(string searchTerm)
        {
            ViewBag.Classes = _context.Classes.ToList();

            List<Scheduling> schedulings = await _context.Schedulings
                .Include(x => x.Hall)
                .Include(x => x.Exam)
                .Include(x => x.Student)
                .ToListAsync();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                schedulings = schedulings.Where(s => s.PaperName.Contains(searchTerm)).ToList();
            }

            return View(schedulings);

        }

        // GET: Schedulings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.Exams = _context.Exams.ToList();
            ViewBag.Halls = _context.Halls.ToList();
            ViewBag.Students = _context.Students.ToList();



            if (id == null || _context.Schedulings == null)
            {
                return NotFound();
            }

            var scheduling = await _context.Schedulings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scheduling == null)
            {
                return NotFound();
            }

            return View(scheduling);
        }
        private bool CheckOverlapping(SchedulingVM scheduling)
        {

            var selectedStudent = _context.Students.FirstOrDefault(s => s.Id == scheduling.StudentId);
            bool checkBefore = false, checkAfter = false;
            var schedulings = _context.Schedulings.Include(x => x.Student).ToList();
            var previousExam = schedulings
                .Where(x => x.Student.ClassId == selectedStudent.ClassId)
                .Where(x => x.ExamDate + x.ExamTime < scheduling.ExamDate + scheduling.ExamTime)
                .OrderByDescending(x => x.ExamDate + x.ExamTime).FirstOrDefault();
            if (previousExam is null)
                checkBefore = true;
            else
            {

                var previousExamStart = previousExam.ExamDate + previousExam.ExamTime;
                var previousExamEnd = previousExamStart + TimeSpan.FromMinutes(previousExam.Duration);
                checkBefore = previousExamEnd < scheduling.ExamDate + scheduling.ExamTime;
            }

            if (checkBefore)
            {
                var nextExam = schedulings
                    .Where(x => x.Student.ClassId == selectedStudent.ClassId)
                    .Where(x => x.ExamDate + x.ExamTime > scheduling.ExamDate + scheduling.ExamTime)
                    .OrderByDescending(x => x.ExamDate + x.ExamTime).FirstOrDefault();
                if (nextExam is null)
                    checkAfter = true;
                else
                {
                    var nextExamStart = nextExam.ExamDate + nextExam.ExamTime;
                    checkAfter = nextExamStart > scheduling.ExamDate + scheduling.ExamTime + TimeSpan.FromMinutes(scheduling.Duration);
                }
            }

            return checkBefore && checkAfter;

        }
        // GET: Schedulings/Create
        public IActionResult Create()
        {
            TempData["Overlapping"] = false;

            ViewBag.Exams = _context.Exams.ToList();
            ViewBag.Halls = _context.Halls.ToList();
            ViewBag.Students = _context.Students.ToList();

            return View();
        }

        // POST: Schedulings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SchedulingVM scheduling)
        {
            var selectedHall = _context.Halls.FirstOrDefault(h => h.Id == scheduling.HallId);
            var selectedExam = selectedHall is not null ? _context.Exams.FirstOrDefault(e => e.Id == scheduling.ExamId) : null;
            var selectedStudent = selectedExam is not null ? _context.Students.FirstOrDefault(s => s.Id == scheduling.StudentId) : null;
            ViewBag.Exams = _context.Exams.ToList();
            ViewBag.Halls = _context.Halls.ToList();
            ViewBag.Students = _context.Students.ToList();


            if (ModelState.IsValid)
            {


                if (CheckOverlapping(scheduling))
                {
                    TempData["Overlapping"] = false;

                    if (selectedHall != null && selectedExam != null && selectedStudent != null)
                    {
                        _context.Schedulings.Add(new Scheduling
                        {
                            PaperName = scheduling.PaperName,
                            ExamDate = scheduling.ExamDate,
                            ExamTime = scheduling.ExamTime,
                            Duration = scheduling.Duration,
                            HallId = selectedHall.Id,
                            StudentId = selectedStudent.Id,
                            ExamId = selectedExam.Id

                        });


                        _context.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    TempData["Overlapping"] = true;
                    return View(scheduling);
                }
            }

            return View(scheduling);
        }

        // GET: Schedulings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Exams = _context.Exams.ToList();
            ViewBag.Halls = _context.Halls.ToList();
            ViewBag.Students = _context.Students.ToList();

            TempData["Overlapping"] = false;

            var scheduling = await _context.Schedulings.FirstOrDefaultAsync(s => s.Id == id);

            if (scheduling != null)
            {
                var viewModel = new SchedulingVM()
                {
                    Id = scheduling.Id,
                    PaperName = scheduling.PaperName,
                    ExamDate = scheduling.ExamDate,
                    ExamTime = scheduling.ExamTime,
                    Duration = scheduling.Duration,
                    HallId = scheduling.HallId,
                    ExamId = scheduling.ExamId,
                    StudentId = scheduling.StudentId,

                };
                return await Task.Run(() => View("Edit", viewModel));

            }

            return RedirectToAction("Index");
        }

        // POST: Schedulings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SchedulingVM updateScheduling)
        {
            ViewBag.Exams = _context.Exams.ToList();
            ViewBag.Halls = _context.Halls.ToList();
            ViewBag.Students = _context.Students.ToList();

            if (id != updateScheduling.Id)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {

                if (CheckOverlapping(updateScheduling))
                {
                    TempData["Overlapping"] = false;

                    var existingUser = _context.Schedulings.FirstOrDefault(s => s.Id == id);
                    if (existingUser != null)
                    {
                        existingUser.PaperName = updateScheduling.PaperName;
                        existingUser.ExamDate = updateScheduling.ExamDate;
                        existingUser.ExamTime = updateScheduling.ExamTime;
                        existingUser.Duration = updateScheduling.Duration;
                        existingUser.HallId = updateScheduling.HallId;
                        existingUser.ExamId = updateScheduling.ExamId;
                        existingUser.StudentId = updateScheduling.StudentId;

                        _context.Update(existingUser);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    TempData["Overlapping"] = true;
                    return View(updateScheduling);
                }
            }
            return View(updateScheduling);
        }

        // GET: Schedulings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.Exams = _context.Exams.ToList();
            ViewBag.Halls = _context.Halls.ToList();
            ViewBag.Students = _context.Students.ToList();

            if (id == null || _context.Schedulings == null)
            {
                return NotFound();
            }

            var scheduling = await _context.Schedulings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scheduling == null)
            {
                return NotFound();
            }

            return View(scheduling);
        }

        // POST: Schedulings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            if (_context.Schedulings == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Schedulings'  is null.");
            }
            var scheduling = await _context.Schedulings.FindAsync(id);
            if (scheduling != null)
            {
                _context.Schedulings.Remove(scheduling);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchedulingExists(int id)
        {
            return _context.Schedulings.Any(e => e.Id == id);
        }
    }
}
