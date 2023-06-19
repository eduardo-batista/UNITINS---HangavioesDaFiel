using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HangavioesDaFiel.Dal;
using HangavioesDaFiel.Models;
using System.Diagnostics;

namespace HangavioesDaFiel.Controllers
{
    public class GaragesController : Controller
    {
        private readonly Context _context;

        public GaragesController(Context context)
        {
            _context = context;
        }

        // GET: Garages
        public async Task<IActionResult> Index()
        {
            var context = _context.Garage.Include(g => g.Airplane);
            return View(await context.ToListAsync());
        }

        // GET: Garages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Garage == null)
            {
                return NotFound();
            }

            var garage = await _context.Garage
                .Include(g => g.Airplane)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (garage == null)
            {
                return NotFound();
            }

            return View(garage);
        }

        // GET: Garages/Create
        public IActionResult Create()
        {
            ViewData["AirplaneId"] = new SelectList(_context.Airplanes, "Id", "Identifier");
            return View();
        }

        // POST: Garages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Identifier,Width,Height,Length,Capacity,Status,AirplaneId")] Garage garage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(garage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AirplaneId"] = new SelectList(_context.Airplanes, "Id", "Identifier", garage.AirplaneId);
            return View(garage);
        }

        // GET: Garages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Garage == null)
            {
                return NotFound();
            }

            var garage = await _context.Garage.FindAsync(id);
            if (garage == null)
            {
                return NotFound();
            }
            ViewData["AirplaneId"] = new SelectList(_context.Airplanes, "Id", "Identifier", garage.AirplaneId);
            return View(garage);
        }

        // POST: Garages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Identifier,Width,Height,Length,Capacity,Status,AirplaneId")] Garage garage)
        {
            if (id != garage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try {                 
                    var airplane = _context.Find<Airplane>(garage.AirplaneId);
                    if ((airplane != null) && !(airplane.ClassSize.Equals(garage.Capacity)))
                    {
                        ModelState.AddModelError("", "Informações incompatíveis");

                        ViewData["AirplaneId"] = new SelectList(_context.Airplanes, "Id", "Identifier", garage.AirplaneId);
                        return View(garage);
                    }
                    if (airplane != null)
                    {
                        airplane.Traveling = false;
                        _context.Update(airplane);
                    }
                    if (garage.Status == false)
                    {
                        garage.AirplaneId = null;
                    }
                    _context.Update(garage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GarageExists(garage.Id))
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
            ViewData["AirplaneId"] = new SelectList(_context.Airplanes, "Id", "Identifier", garage.AirplaneId);
            return View(garage);
        }

        // GET: Garages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Garage == null)
            {
                return NotFound();
            }

            var garage = await _context.Garage
                .Include(g => g.Airplane)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (garage == null)
            {
                return NotFound();
            }

            return View(garage);
        }

        // POST: Garages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Garage == null)
            {
                return Problem("Entity set 'Context.Garage'  is null.");
            }
            var garage = await _context.Garage.FindAsync(id);
            if (garage != null)
            {
                _context.Garage.Remove(garage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GarageExists(int id)
        {
          return (_context.Garage?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
