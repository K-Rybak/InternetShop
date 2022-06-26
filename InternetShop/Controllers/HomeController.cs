using InternetShop.Data;
using InternetShop.Models;
using InternetShop.Models.ViewModel;
using InternetShop.Utils;
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
            IEnumerable<Game> gameList = _db.Game.Include(u => u.Category).Include(u => u.Publisher);

            HomeVM homeVM = new HomeVM()
            {
                Games = gameList,
                Categories = _db.Category
            };

            return View(homeVM);
        }

        public IActionResult Details(int id)
        {
            var shoppingCartList = new List<ShoppingCart>();

            var sessionState = HttpContext.Session.Get<List<ShoppingCart>>(WebConst.SessionCart);
            if (sessionState != null && sessionState.Count > 0)
                shoppingCartList = sessionState;

            DetailsVM detailsVM = new DetailsVM()
            {
                Game = _db.Game.Include(u => u.Category).Include(u => u.Publisher)
                .Where(u => u.Id == id).FirstOrDefault(),
                IsExistsInCart = false
            };

            foreach (var item in shoppingCartList)
            {
                if (item.GameId == id)
                {
                    detailsVM.IsExistsInCart = true;
                }
            }

            return View(detailsVM);
        }

        [HttpPost, ActionName("Details")]
        public IActionResult DetailsPost(int id)
        {
            var shoppingCartList = new List<ShoppingCart>();

            var sessionState = HttpContext.Session.Get<List<ShoppingCart>>(WebConst.SessionCart);
            if (sessionState != null && sessionState.Count > 0)
                shoppingCartList = sessionState;

            shoppingCartList.Add(new ShoppingCart { GameId = id });
            HttpContext.Session.Set(WebConst.SessionCart, shoppingCartList);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveFromCart(int id)
        {
            var shoppingCartList = new List<ShoppingCart>();

            var sessionState = HttpContext.Session.Get<List<ShoppingCart>>(WebConst.SessionCart);
            if (sessionState != null && sessionState.Count > 0)
                shoppingCartList = sessionState;

            var itemToDelete = shoppingCartList.FirstOrDefault(i => i.GameId == id);
            if (itemToDelete != null)
            {
                shoppingCartList.Remove(itemToDelete);
            }

            HttpContext.Session.Set(WebConst.SessionCart, shoppingCartList);
            return RedirectToAction(nameof(Index));
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
