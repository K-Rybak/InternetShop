using InternetShop.Data;
using InternetShop.Models;
using InternetShop.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace InternetShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _db;

        public HomeController(ILogger<HomeController> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            // Возвращает null модели Publisher
            // IEnumerable<Game> gameList = _db.Game.Include(u => u.Category).Unclude(u => u.Publisher);

            IEnumerable<Game> gameList = _db.Game;

            foreach (var item in gameList)
            {
                item.Category = _db.Category.FirstOrDefault(u => u.Id == item.CategoryId);
                item.Publisher = _db.Publisher.FirstOrDefault(u => u.Id == item.PublisherId);
            }

            HomeVM homeVM = new HomeVM()
            {
                Games = gameList,
                Categories = _db.Category
            };

            return View(homeVM);
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
