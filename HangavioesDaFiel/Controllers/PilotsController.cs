using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HangavioesDaFiel.Dal;
using HangavioesDaFiel.Models;

namespace HangavioesDaFiel.Controllers
{
    public class PilotsController : Controller
    {
        private readonly Context _context;

        public PilotsController(Context context)
        {
            _context = context;
        }

        // GET: Pilots
        public async Task<IActionResult> Index()
        {
              return _context.Pilot != null ? 
                          View(await _context.Pilot.ToListAsync()) :
                          Problem("Entity set 'Context.Pilot'  is null.");
        }

        // GET: Pilots/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pilot == null)
            {
                return NotFound();
            }

            var pilot = await _context.Pilot
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pilot == null)
            {
                return NotFound();
            }

            return View(pilot);
        }

        // GET: Pilots/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pilots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Aptitude,Registration,Id,Name,Cpf,Phone,Email,Status")] Pilot pilot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pilot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pilot);
        }

        // GET: Pilots/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pilot == null)
            {
                return NotFound();
            }

            var pilot = await _context.Pilot.FindAsync(id);
            if (pilot == null)
            {
                return NotFound();
            }
            return View(pilot);
        }

        // POST: Pilots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Aptitude,Registration,Id,Name,Cpf,Phone,Email,Status")] Pilot pilot)
        {
            if (id != pilot.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pilot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PilotExists(pilot.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pilot);
        }

        // GET: Pilots/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pilot == null)
            {
                return NotFound();
            }

            var pilot = await _context.Pilot
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pilot == null)
            {
                return NotFound();
            }

            return View(pilot);
        }

        // POST: Pilots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pilot == null)
            {
                return Problem("Entity set 'Context.Pilot'  is null.");
            }
            var pilot = await _context.Pilot.FindAsync(id);
            if (pilot != null)
            {
                _context.Pilot.Remove(pilot);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PilotExists(int id)
        {
          return (_context.Pilot?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
