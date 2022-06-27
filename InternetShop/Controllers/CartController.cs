using InternetShop.Data;
using InternetShop.Models;
using InternetShop.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetShop.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _db;

        public CartController(AppDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            List<ShoppingCart> shoppingCarts = new List<ShoppingCart>();
            var sessionState = HttpContext.Session.Get<List<ShoppingCart>>(WebConst.SessionCart);

            if (sessionState != null && sessionState.Count > 0)
            {
                shoppingCarts = sessionState;
            }

            List<int> gameInCart = shoppingCarts.Select(i => i.GameId).ToList();
            IEnumerable<Game> gameList = _db.Game.Where(u => gameInCart.Contains(u.Id));

            return View(gameList);
        }

        public IActionResult Remove(int id)
        {
            List<ShoppingCart> shoppingCartsList = new List<ShoppingCart>();
            var sessionState = HttpContext.Session.Get<List<ShoppingCart>>(WebConst.SessionCart);

            if (sessionState != null && sessionState.Count > 0)
            {
                shoppingCartsList = sessionState;
            }

            shoppingCartsList.Remove(shoppingCartsList.FirstOrDefault(u => u.GameId == id));

            HttpContext.Session.Set(WebConst.SessionCart, shoppingCartsList);

            return RedirectToAction(nameof(Index));
        }
    }
}
