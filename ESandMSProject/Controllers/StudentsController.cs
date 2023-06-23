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
using System.Security.Claims;

namespace ESandMSProject.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Students
        [Authorize(Roles = "A")]
        public async Task<IActionResult> Index(string searchTerm)
        {
            ViewBag.Classes = _context.Classes.ToList();
            ViewBag.Logins = _context.Logins.Where(l => l.Roles == "S").ToList();


            List<Student> student = await _context.Students
                .Include(x => x.Class)
                .Include(x => x.Login) // LoginId(FK) has been included.
                .ToListAsync();

            
            if (!string.IsNullOrEmpty(searchTerm))
            {
                student = student.Where(s => s.Name.Contains(searchTerm)).ToList();
            }

            return View(student);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            ViewBag.Classes = _context.Classes.ToList();
            ViewBag.Logins = _context.Logins.Where(l => l.Roles == "S").ToList();
            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewBag.Classes = _context.Classes.ToList();
            ViewBag.Logins = _context.Logins.Where(l => l.Roles == "S").ToList();

            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentVM student)
        {

            if (ModelState.IsValid)
            {
                var selectedClass = _context.Classes.FirstOrDefault(c => c.Id == student.ClassId);
                var selectedLogin = _context.Logins.FirstOrDefault(l => l.Id == student.LoginId);


                if (selectedClass != null && selectedLogin != null)
                {
                    _context.Students.Add(new Student
                    {
                        Name = student.Name,
                        Surname = student.Surname,
                        SchoolNumber = student.SchoolNumber,
                        ClassId = selectedClass.Id,
                        LoginId = selectedLogin.Id
                    });


                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            // Sınıf bulunamazsa, ModelState hatası ekleyin
            ModelState.AddModelError("Class.Id", "The entered class number is invalid.");
            ModelState.AddModelError("Login.Id", "The entered login number is invalid.");
            ViewBag.Classes = _context.Classes.ToList();
            ViewBag.Logins = _context.Logins.Where(l => l.Roles == "S").ToList();
            return View(student);
        }

        // GET: Students/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Classes = _context.Classes.ToList();
            ViewBag.Logins = _context.Logins.Where(l => l.Roles == "S").ToList();
            var student = await _context.Students.FirstOrDefaultAsync(c => c.Id == id);
            if (student != null)
            {
                var viewModel = new StudentVM()
                {
                    Id = student.Id,
                    Name = student.Name,
                    Surname = student.Surname,
                    SchoolNumber = student.SchoolNumber,
                    ClassId = student.ClassId,
                    LoginId = student.LoginId,

                };
                return await Task.Run(() => View("Edit", viewModel));

            }
            return RedirectToAction("Index");

        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentVM updateStudent)
        {

            if (id != updateStudent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingUser = _context.Students.FirstOrDefault(u => u.Id == id);
                if (existingUser != null)
                {
                    existingUser.ClassId = updateStudent.ClassId;
                    existingUser.Name = updateStudent.Name;
                    existingUser.Surname = updateStudent.Surname;
                    existingUser.SchoolNumber = updateStudent.SchoolNumber;
                    existingUser.LoginId = updateStudent.LoginId;   

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return NotFound();
                }
            }
            return View(updateStudent);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            ViewBag.Classes = _context.Classes.ToList();
            ViewBag.Logins = _context.Logins.Where(l => l.Roles == "S").ToList();

            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);

        }


        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}

