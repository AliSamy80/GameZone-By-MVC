using GameZone.Models;
using GameZone.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GameZone.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGamesService gamesService;           // Service (1)

        public HomeController(IGamesService _gamesService)
        {
            gamesService = _gamesService;
        }

        public IActionResult Index()
        {
            var games = gamesService.GetAllGames();
            return View(games);
        }

   
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}