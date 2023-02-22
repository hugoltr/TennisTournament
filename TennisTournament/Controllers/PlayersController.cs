using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TennisTournament.Entities;
using TennisTournament.Seedwork;

namespace TennisTournament.Controllers
{
    public class PlayersController : Controller
    {

        private IHttpClientFactory HttpClientFactory { get; }
        private readonly IWebHostEnvironment HostingEnvironment;

        public PlayersController(IHttpClientFactory httpClientFactory, IWebHostEnvironment hostingEnvironment)
        {
            HttpClientFactory = httpClientFactory;
            HostingEnvironment = hostingEnvironment;
        }

        // GET: Players
        public async Task<IActionResult> Index()
        {
            HttpClient httpClient = HttpClientFactory.CreateClient("API");
            var players = await httpClient.GetFromJsonAsync<IEnumerable<Player>>("api/Players");
            return View(players);
        }

        // GET: Players/Details/5
        public async Task<IActionResult> Details(int id)
        {
            using HttpClient httpClient = HttpClientFactory.CreateClient("API");
            var player = await httpClient.GetFromJsonAsync<Player>($"api/Players/{id}");
            return View(player);
        }

        // GET: Players/Create
        public IActionResult Create()
        {
            var NatLit = new List<SelectListItem>(); 
            foreach (var eVal in Enum.GetValues(typeof(TennisTournament.Seedwork.Nationality))) 
            { 
                NatLit.Add(new SelectListItem 
                { 
                    Text = string.Join(" ", Enum.GetName(typeof(TennisTournament.Seedwork.Nationality), eVal).Split("_")), Value = eVal.ToString() 
                }); 
            }
            ViewBag.Nationalities = NatLit;
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Player player)
        {
            if (ModelState.IsValid)
            {
                using HttpClient httpClient = HttpClientFactory.CreateClient("API");
                var response = await httpClient.PostAsJsonAsync("api/Players", player);

                if(response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Erreur lors de la création du joueur.");
                }
            }
            return View(player);
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            using HttpClient httpClient = HttpClientFactory.CreateClient("API");
            var player = await httpClient.GetFromJsonAsync<Player>($"api/Players/{id}");
            if (player == null) 
            {
                return NotFound();
            }
            else
            {
                return View(player);
            }
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Player player)
        {
            if (id != player.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using HttpClient httpClient = HttpClientFactory.CreateClient("API");
                var response = await httpClient.PutAsJsonAsync($"api/Players/{id}", player);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Erreur lors de la modification du joueur.");
                }
            }
            return View(player);
        }

        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            using HttpClient httpClient = HttpClientFactory.CreateClient("API");
            var player = await httpClient.GetFromJsonAsync<Player>($"api/Players/{id}");
            if(player == null)
            {
                return NotFound();
            }
            else
            {
                return View(player);
            }
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using HttpClient httpClient = HttpClientFactory.CreateClient("API");
            var response = await httpClient.DeleteAsync($"api/Players/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return BadRequest("Erreur lors de la suppression du joueur.");
            }
        }
    }
}
