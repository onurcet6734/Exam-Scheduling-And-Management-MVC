using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ESandMSProject.Models;
using ESandMSProject.Models.Domain;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace ESandMSProject.Controllers
{
    public class HallsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HallsController(ApplicationDbContext context)
        {
            _context = context;

        }
        // GET: Halls
        [Authorize(Roles = "A")]

        public async Task<IActionResult> Index(string searchTerm)
        {
            var halls = _context.Halls.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                halls = halls.Where(h => h.Name.Contains(searchTerm));
            }
            return View((await halls.ToListAsync(), new Hall())); 
        }
        // GET: Halls/Details/id
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Halls == null)
            {
                return NotFound();
            }
            var hall = await _context.Halls.FindAsync(id);
            if (hall == null)
            {
                return NotFound();
            }
            return View(hall);
        }
        // GET: Halls/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: Halls/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Hall hall) 
        {

            if (ModelState.IsValid)
            {
                _context.Add(hall);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hall);
        }
        // GET: Halls/Edit/id
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Halls == null)
            {
                return NotFound();
            }
            var hall = await _context.Halls.FindAsync(id);
            if (hall == null)
            {
                return NotFound();
            }
            return View(hall);
            //return View(hall);
        }
        // POST: Halls/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,NumberOfSeats")] Hall hall)
        {
            if (id != hall.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hall);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HallExists(hall.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(hall);
        }
        // GET: Halls/Delete/id
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Halls == null)
            {
                return NotFound();
            }
            var hall = await _context.Halls
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hall == null)
            {
                return NotFound();
            }
            return View(hall);
        }
        // POST: Halls/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Halls == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Halls'  is null.");
            }
            var hall = await _context.Halls.FindAsync(id);
            if (hall != null)
            {
                _context.Halls.Remove(hall);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        private bool HallExists(int id)
        {
            return _context.Halls.Any(e => e.Id == id);
        }
    }
}
