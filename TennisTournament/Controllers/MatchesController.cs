using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TennisTournament.Entities;
using TennisTournament.Models;
using TennisTournament.Models.Matchs;
using TennisTournament.Seedwork;

namespace TennisTournament.Controllers
{
    public class MatchesController : Controller
    {
        private IHttpClientFactory HttpClientFactory { get; }

        public MatchesController(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
        }

        private List<SelectListItem> SetSelectListItem(IEnumerable objects)
        {
            var list = new List<SelectListItem>();
            foreach (Entity item in objects)
            {
                list.Add(new SelectListItem { Text = $"{item.FirstName} {item.LastName}", Value = item.ID.ToString() });
            }
            return list;

        }

        private async Task SetListItem()
        {
            using HttpClient httpClient = HttpClientFactory.CreateClient("API");
            var courts = await httpClient.GetFromJsonAsync<IEnumerable<Court>>("api/Courts");

            var courtList = new List<SelectListItem>();
            foreach (Court court in courts)
            {
                courtList.Add(new SelectListItem { Text = court.Name, Value = court.ID.ToString() });
            }

            var tournaments = await httpClient.GetFromJsonAsync<IEnumerable<Tournament>>("api/Tournaments");
            var tournamentList = new List<SelectListItem>();
            foreach (Tournament tournament in tournaments)
            {
                tournamentList.Add(new SelectListItem { Text = tournament.Name, Value = tournament.ID.ToString() });
            }

            ViewBag.courtList = courtList;
            ViewBag.tournamentList = tournamentList;

            var players = await httpClient.GetFromJsonAsync<IEnumerable<Player>>("api/Players");
            ViewBag.playersList = SetSelectListItem(players);
            var referees = await httpClient.GetFromJsonAsync<IEnumerable<Referee>>("api/Referees");
            ViewBag.refereesList = SetSelectListItem(referees);

        }

        // GET: Matches
        public async Task<IActionResult> Index()
        {
            using HttpClient httpClient = HttpClientFactory.CreateClient("API");
            var matches = await httpClient.GetFromJsonAsync<IEnumerable<Match>>("api/matches");
            return View(matches);
        }

        //GET: Matches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using HttpClient httpClient = HttpClientFactory.CreateClient("API");
            var match = await httpClient.GetFromJsonAsync<Match>($"api/matches/{id}");

            return View(match);
        }

        //GET: Matches/Create
        public async Task<IActionResult> Create()
        {
            var matchCreateViewModel = new MatchCreateViewModel();
            await this.SetListItem();

            return View(matchCreateViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MatchCreateViewModel matchCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                using HttpClient httpClient = HttpClientFactory.CreateClient("API");
                var firstPlayer = await httpClient.GetFromJsonAsync<Player>($"api/Players/{matchCreateViewModel.FirstPlayerID}");
                var secondPlayer = await httpClient.GetFromJsonAsync<Player>($"api/Players/{matchCreateViewModel.SecondPlayerID}");
                var referee = await httpClient.GetFromJsonAsync<Referee>($"api/Referees/{matchCreateViewModel.RefereeID}");
                var tournament = await httpClient.GetFromJsonAsync<Tournament>($"api/Tournaments/{matchCreateViewModel.TournamentID}");
                var court = await httpClient.GetFromJsonAsync<Court>($"api/Courts/{matchCreateViewModel.CourtID}");

                var match = new Match()
                {
                    FirstPlayer = firstPlayer,
                    SecondPlayer = secondPlayer,
                    Referee = referee,
                    Tournament = tournament,
                    Court = court
                };
                var response = await httpClient.PostAsJsonAsync("api/Matches", match);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                return BadRequest();
            }

            return View(matchCreateViewModel);
        }

        // GET: Matches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            using HttpClient httpClient = HttpClientFactory.CreateClient("API");
            var match = await httpClient.GetFromJsonAsync<MatchEditViewModel>($"api/Matches/{id}");
            if(match == null)
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
        public async Task<IActionResult> Edit(int id, MatchEditViewModel editViewModel)
        {
            if (id != editViewModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                using HttpClient httpClient = HttpClientFactory.CreateClient("API");
                var match = await httpClient.GetFromJsonAsync<Match>($"api/Matches/{id}");
                if(match == null)
                {
                    return NotFound();
                }

                match.StartingDate = editViewModel.StartingDate;

                var response = await httpClient.PutAsJsonAsync($"api/Matches/{id}", match);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                return BadRequest();
            }
            return View(editViewModel);
        }

        // GET: Matches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            using HttpClient httpClient = HttpClientFactory.CreateClient("API");
            var match = await httpClient.GetFromJsonAsync<Match>($"api/Matches/{id}");
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
            using HttpClient httpClient = HttpClientFactory.CreateClient("API");
            var response = await httpClient.DeleteAsync($"api/Matches/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest();
        }

        //private bool MatchExists(int id)
        //{
        //    return _context.Matchs.Any(e => e.ID == id);
        //}
    }
}
