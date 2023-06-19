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
    public class TravelsController : Controller
    {
        private readonly Context _context;

        public TravelsController(Context context)
        {
            _context = context;
        }

        // GET: Travels
        public async Task<IActionResult> Index()
        {
            var context = _context.Travel.Include(t => t.Airplane).Include(t => t.Copilot).Include(t => t.Pilot).Include(t => t.Team);
            return View(await context.ToListAsync());
        }

        // GET: Travels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Travel == null)
            {
                return NotFound();
            }

            var travel = await _context.Travel
                .Include(t => t.Airplane)
                .Include(t => t.Copilot)
                .Include(t => t.Pilot)
                .Include(t => t.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (travel == null)
            {
                return NotFound();
            }

            return View(travel);
        }

        // GET: Travels/Create
        public IActionResult Create()
        {
            ViewData["AirplaneId"] = new SelectList(_context.Airplanes, "Id", "Identifier");
            ViewData["CopilotId"] = new SelectList(_context.Pilot, "Id", "Name");
            ViewData["PilotId"] = new SelectList(_context.Pilot, "Id", "Name");
            ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Identifier");
            return View();
        }

        // POST: Travels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DepartureDate,ReturnDate,TeamId,PilotId,CopilotId,AirplaneId")] Travel travel)
        {
            if (ModelState.IsValid)
            {
                var pilot = _context.Find<Pilot>(travel.PilotId);
                var copilot = _context.Find<Pilot>(travel.CopilotId);
                var airplane = _context.Find<Airplane>(travel.AirplaneId);
                var team = _context.Find<Team>(travel.TeamId);

                if (pilot.Status == false)
                {
                    ModelState.AddModelError("", "Piloto Inativo");
                    ViewData["AirplaneId"] = new SelectList(_context.Airplanes, "Id", "Identifier", travel.AirplaneId);
                    ViewData["CopilotId"] = new SelectList(_context.Pilot, "Id", "Name", travel.CopilotId);
                    ViewData["PilotId"] = new SelectList(_context.Pilot, "Id", "Name", travel.PilotId);
                    ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Identifier", travel.TeamId);
                    return View(travel);
                } 
                else if (copilot.Status == false)
                {
                    ModelState.AddModelError("", "Copiloto Inativo");
                    ViewData["AirplaneId"] = new SelectList(_context.Airplanes, "Id", "Identifier", travel.AirplaneId);
                    ViewData["CopilotId"] = new SelectList(_context.Pilot, "Id", "Name", travel.CopilotId);
                    ViewData["PilotId"] = new SelectList(_context.Pilot, "Id", "Name", travel.PilotId);
                    ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Identifier", travel.TeamId);
                    return View(travel);
                }
                else if(airplane.Status == false)
                {
                    ModelState.AddModelError("", "Avião Inativo");
                    ViewData["AirplaneId"] = new SelectList(_context.Airplanes, "Id", "Identifier", travel.AirplaneId);
                    ViewData["CopilotId"] = new SelectList(_context.Pilot, "Id", "Name", travel.CopilotId);
                    ViewData["PilotId"] = new SelectList(_context.Pilot, "Id", "Name", travel.PilotId);
                    ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Identifier", travel.TeamId);
                    return View(travel);
                }
                else if(team.Status == false)
                {
                    ModelState.AddModelError("", "Equipe Inativa");
                    ViewData["AirplaneId"] = new SelectList(_context.Airplanes, "Id", "Identifier", travel.AirplaneId);
                    ViewData["CopilotId"] = new SelectList(_context.Pilot, "Id", "Name", travel.CopilotId);
                    ViewData["PilotId"] = new SelectList(_context.Pilot, "Id", "Name", travel.PilotId);
                    ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Identifier", travel.TeamId);
                    return View(travel);
                }
                else if (airplane.Traveling)
                {
                    ModelState.AddModelError("", "Avião já em viagem");
                    ViewData["AirplaneId"] = new SelectList(_context.Airplanes, "Id", "Identifier", travel.AirplaneId);
                    ViewData["CopilotId"] = new SelectList(_context.Pilot, "Id", "Name", travel.CopilotId);
                    ViewData["PilotId"] = new SelectList(_context.Pilot, "Id", "Name", travel.PilotId);
                    ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Identifier", travel.TeamId);
                    return View(travel);
                }
                else if (pilot!.Aptitude.Equals(airplane!.ClassMotor) &&
                team!.Aptitude.Equals(airplane!.ClassMotor) &&
                ((copilot == null && (airplane!.ClassMotor.Equals("AM1") || airplane!.ClassMotor.Equals("AM2"))) ||
                (copilot != null && copilot.Aptitude.Equals(airplane!.ClassMotor))))
                {
                    airplane.Traveling = true;
                    _context.Update(airplane);
                    _context.Add(travel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Informações incompatíveis");
                    ViewData["AirplaneId"] = new SelectList(_context.Airplanes, "Id", "Identifier", travel.AirplaneId);
                    ViewData["CopilotId"] = new SelectList(_context.Pilot, "Id", "Name", travel.CopilotId);
                    ViewData["PilotId"] = new SelectList(_context.Pilot, "Id", "Name", travel.PilotId);
                    ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Identifier", travel.TeamId);
                    return View(travel);
                }
            }
            ViewData["AirplaneId"] = new SelectList(_context.Airplanes, "Id", "ClassHeight", travel.AirplaneId);
            ViewData["CopilotId"] = new SelectList(_context.Pilot, "Id", "Aptitude", travel.CopilotId);
            ViewData["PilotId"] = new SelectList(_context.Pilot, "Id", "Aptitude", travel.PilotId);
            ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Aptitude", travel.TeamId);
            return View(travel);
        }

        // GET: Travels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Travel == null)
            {
                return NotFound();
            }

            var travel = await _context.Travel.FindAsync(id);
            if (travel == null)
            {
                return NotFound();
            }
            ViewData["AirplaneId"] = new SelectList(_context.Airplanes, "Id", "Identifier", travel.AirplaneId);
            ViewData["CopilotId"] = new SelectList(_context.Pilot, "Id", "Name", travel.CopilotId);
            ViewData["PilotId"] = new SelectList(_context.Pilot, "Id", "Name", travel.PilotId);
            ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Identifier", travel.TeamId);
            return View(travel);
        }

        // POST: Travels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DepartureDate,ReturnDate,TeamId,PilotId,CopilotId,AirplaneId")] Travel travel)
        {
            if (id != travel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var pilot = _context.Find<Pilot>(travel.PilotId);
                    var copilot = _context.Find<Pilot>(travel.CopilotId);
                    var airplane = _context.Find<Airplane>(travel.AirplaneId);
                    var team = _context.Find<Team>(travel.TeamId);

                    if (pilot.Status == false)
                    {
                        ModelState.AddModelError("", "Piloto Inativo");
                        ViewData["AirplaneId"] = new SelectList(_context.Airplanes, "Id", "Identifier", travel.AirplaneId);
                        ViewData["CopilotId"] = new SelectList(_context.Pilot, "Id", "Name", travel.CopilotId);
                        ViewData["PilotId"] = new SelectList(_context.Pilot, "Id", "Name", travel.PilotId);
                        ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Identifier", travel.TeamId);
                        return View(travel);
                    }
                    else if (copilot.Status == false)
                    {
                        ModelState.AddModelError("", "Copiloto Inativo");
                        ViewData["AirplaneId"] = new SelectList(_context.Airplanes, "Id", "Identifier", travel.AirplaneId);
                        ViewData["CopilotId"] = new SelectList(_context.Pilot, "Id", "Name", travel.CopilotId);
                        ViewData["PilotId"] = new SelectList(_context.Pilot, "Id", "Name", travel.PilotId);
                        ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Identifier", travel.TeamId);
                        return View(travel);
                    }
                    else if (airplane.Status == false)
                    {
                        ModelState.AddModelError("", "Avião Inativo");
                        ViewData["AirplaneId"] = new SelectList(_context.Airplanes, "Id", "Identifier", travel.AirplaneId);
                        ViewData["CopilotId"] = new SelectList(_context.Pilot, "Id", "Name", travel.CopilotId);
                        ViewData["PilotId"] = new SelectList(_context.Pilot, "Id", "Name", travel.PilotId);
                        ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Identifier", travel.TeamId);
                        return View(travel);
                    }
                    else if (team.Status == false)
                    {
                        ModelState.AddModelError("", "Equipe Inativa");
                        ViewData["AirplaneId"] = new SelectList(_context.Airplanes, "Id", "Identifier", travel.AirplaneId);
                        ViewData["CopilotId"] = new SelectList(_context.Pilot, "Id", "Name", travel.CopilotId);
                        ViewData["PilotId"] = new SelectList(_context.Pilot, "Id", "Name", travel.PilotId);
                        ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Identifier", travel.TeamId);
                        return View(travel);
                    }
                    else if (airplane.Traveling)
                    {
                        ModelState.AddModelError("", "Avião já em viagem");
                        ViewData["AirplaneId"] = new SelectList(_context.Airplanes, "Id", "Identifier", travel.AirplaneId);
                        ViewData["CopilotId"] = new SelectList(_context.Pilot, "Id", "Name", travel.CopilotId);
                        ViewData["PilotId"] = new SelectList(_context.Pilot, "Id", "Name", travel.PilotId);
                        ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Identifier", travel.TeamId);
                        return View(travel);
                    }
                    else if (pilot!.Aptitude.Equals(airplane!.ClassMotor) &&
                    team!.Aptitude.Equals(airplane!.ClassMotor) &&
                    ((copilot == null && (airplane!.ClassMotor.Equals("AM1") || airplane!.ClassMotor.Equals("AM2"))) ||
                    (copilot != null && copilot.Aptitude.Equals(airplane!.ClassMotor))))
                    {
                        airplane.Traveling = true;
                        _context.Update(airplane);
                        _context.Update(travel);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Informações incompatíveis");
                        ViewData["AirplaneId"] = new SelectList(_context.Airplanes, "Id", "Identifier", travel.AirplaneId);
                        ViewData["CopilotId"] = new SelectList(_context.Pilot, "Id", "Name", travel.CopilotId);
                        ViewData["PilotId"] = new SelectList(_context.Pilot, "Id", "Name", travel.PilotId);
                        ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Identifier", travel.TeamId);
                        return View(travel);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TravelExists(travel.Id))
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
            ViewData["AirplaneId"] = new SelectList(_context.Airplanes, "Id", "Identifier", travel.AirplaneId);
            ViewData["CopilotId"] = new SelectList(_context.Pilot, "Id", "Name", travel.CopilotId);
            ViewData["PilotId"] = new SelectList(_context.Pilot, "Id", "Name", travel.PilotId);
            ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Identifier", travel.TeamId);
            return View(travel);
        }

        // GET: Travels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Travel == null)
            {
                return NotFound();
            }

            var travel = await _context.Travel
                .Include(t => t.Airplane)
                .Include(t => t.Copilot)
                .Include(t => t.Pilot)
                .Include(t => t.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (travel == null)
            {
                return NotFound();
            }

            return View(travel);
        }

        // POST: Travels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Travel == null)
            {
                return Problem("Entity set 'Context.Travel'  is null.");
            }
            var travel = await _context.Travel.FindAsync(id);
            if (travel != null)
            {
                _context.Travel.Remove(travel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TravelExists(int id)
        {
          return (_context.Travel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
