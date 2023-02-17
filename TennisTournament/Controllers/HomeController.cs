using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TennisTournament.Data;
using TennisTournament.Models;

namespace TennisTournament.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public TennisContext Context { get; }

        public HomeController(ILogger<HomeController> logger, TennisContext context)
        {
            _logger = logger;
            Context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}