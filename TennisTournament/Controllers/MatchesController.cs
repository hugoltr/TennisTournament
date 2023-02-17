using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TennisTournament.Data;
using TennisTournament.Entities;
using TennisTournament.Models;

namespace TennisTournament.Controllers
{
    public class MatchesController : Controller
    {
        private readonly TennisContext _context;

        public MatchesController(TennisContext context)
        {
            _context = context;
        }

        // GET: Matches
        public async Task<IActionResult> Index()
        {
              return View(await _context.Matchs
                  .Include(p1 => p1.FirstPlayer)
                  .Include(p2 => p2.SecondPlayer)
                  .ToListAsync());
        }

        // GET: Matches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Matchs == null)
            {
                return NotFound();
            }

            var match = await _context.Matchs
                .Include (p1 => p1.FirstPlayer)
                .Include (p2 => p2.SecondPlayer)
                .Include (c => c.Court)
                .Include (r => r.Referee)
                .Include (t => t.Tournament)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // GET: Matches/Create
        public IActionResult Create()
        {
            var matchCreateViewModel = new MatchCreateViewModel();

            var refereeList = new List<SelectListItem>();
            foreach(Referee referee in _context.Referees)
            {
                refereeList.Add(new SelectListItem { Text = $"{referee.FirstName} {referee.LastName}", Value = referee.ID.ToString() });
            }

            var playerList = new List<SelectListItem>();
            foreach (Player player in _context.Players)
            {
                playerList.Add(new SelectListItem { Text = $"{player.FirstName} {player.LastName}", Value = player.ID.ToString() });
            }

            var courtList = new List<SelectListItem>();
            foreach(Court court in _context.Courts)
            {
                courtList.Add(new SelectListItem { Text = court.Name, Value = court.ID.ToString() });
            }

            var tournamentList = new List<SelectListItem>();
            foreach(Tournament tournament in _context.Tournaments)
            {
                tournamentList.Add(new SelectListItem { Text = tournament.Name, Value=tournament.ID.ToString() });
            }

            ViewBag.refereesList = refereeList;
            ViewBag.courtList = courtList;
            ViewBag.tournamentList = tournamentList;
            ViewBag.playersList = playerList;


            return View(matchCreateViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MatchCreateViewModel matchCreateViewModel)
        {
            if(matchCreateViewModel.FirstPlayerID == matchCreateViewModel.SecondPlayerID)
            {
                ViewData["error"] = "Impossible de sélectionner le même joueur";
                return View(matchCreateViewModel);
            }
            else 
            {
                if (ModelState.IsValid)
                {
                    Match match = new Match()
                    {
                        StartingDate = matchCreateViewModel.StartingDate,
                        FirstPlayer = await _context.Players.FirstOrDefaultAsync(p1 => p1.ID == matchCreateViewModel.FirstPlayerID),
                        SecondPlayer = await _context.Players.FirstOrDefaultAsync(p2 => p2.ID == matchCreateViewModel.SecondPlayerID),
                        Referee = await _context.Referees.FirstOrDefaultAsync(r => r.ID == matchCreateViewModel.RefereeID),
                        Court = await _context.Courts.FirstOrDefaultAsync(c => c.ID == matchCreateViewModel.CourtID),
                        Tournament = await _context.Tournaments.FirstOrDefaultAsync(t => t.ID == matchCreateViewModel.TournamentID)


                    };
                    _context.Add(match);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(matchCreateViewModel);
            }
            
        }

        // GET: Matches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Matchs == null)
            {
                return NotFound();
            }

            var match = await _context.Matchs.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }
            return View(match);
        }

        // POST: Matches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,StartingDate")] Match match)
        {
            if (id != match.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(match);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchExists(match.ID))
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
            return View(match);
        }

        // GET: Matches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Matchs == null)
            {
                return NotFound();
            }

            var match = await _context.Matchs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Matchs == null)
            {
                return Problem("Entity set 'TennisContext.Matchs'  is null.");
            }
            var match = await _context.Matchs.FindAsync(id);
            if (match != null)
            {
                _context.Matchs.Remove(match);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchExists(int id)
        {
          return _context.Matchs.Any(e => e.ID == id);
        }
    }
}
