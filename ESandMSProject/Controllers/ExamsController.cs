using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ESandMSProject.Models;
using ESandMSProject.Models.Domain;
using ESandMSProject.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace ESandMSProject.Controllers
{
    public class ExamsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExamsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Exams
        [Authorize(Roles = "A")]

        public async Task<IActionResult> Index(string searchTerm)
        {


            List<Exam> exam = await _context.Exams.Include(x => x.Class).ToListAsync();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                exam = exam.Where(e => e.Name.Contains(searchTerm)).ToList();
            }

            return View(exam);


        }

        // GET: Exams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.Classes = _context.Classes.ToList();


            if (id == null || _context.Exams == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // GET: Exams/Create
        public IActionResult Create()
        {
            ViewBag.Classes = _context.Classes.ToList();
            return View();
        }

        // POST: Exams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExamVM exam)
        {    
            if (ModelState.IsValid)
            {
                var selectedExams = _context.Classes.FirstOrDefault(e => e.Id == exam.ClassId);
                if(selectedExams != null)
                {
                    selectedExams.Exams.Add(new Exam
                    {
                        Name = exam.Name

                    });
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError("Class.Id", "The entered class number is invalid.");
            ViewBag.Classes = _context.Classes.ToList();
            return View(exam);
        }

        // GET: Exams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Classes = _context.Classes.ToList();
            var exams = await _context.Exams.FirstOrDefaultAsync(e => e.Id == id);

            if (exams != null)
            {
                var viewModel = new ExamVM()
                {
                    Id = exams.Id,
                    Name = exams.Name,
                    ClassId= exams.ClassId

                };
                return await Task.Run(() => View("Edit", viewModel));

            }

            return RedirectToAction("Index");
        }

        // POST: Exams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ExamVM updateExam)
        {
            if (id != updateExam.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingExam = _context.Exams.FirstOrDefault(u => u.Id == id);
                if (existingExam != null)
                {
                    existingExam.ClassId = updateExam.ClassId;
                    existingExam.Name = updateExam.Name;

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return NotFound();
                }
            }
            return View(updateExam);
        }

        // GET: Exams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.Classes = _context.Classes.ToList();


            if (id == null || _context.Exams == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // POST: Exams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Exams == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Exams'  is null.");
            }
            var exam = await _context.Exams.FindAsync(id);
            if (exam != null)
            {
                _context.Exams.Remove(exam);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamExists(int id)
        {
          return _context.Exams.Any(e => e.Id == id);
        }
    }
}
