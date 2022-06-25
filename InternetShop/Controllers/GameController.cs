using InternetShop.Data;
using InternetShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetShop.Controllers
{
    public class GameController : Controller
    {
        private readonly AppDbContext _db;

        public GameController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Game> gameList = _db.Game;

            foreach (var item in gameList)
            {
                item.Category = _db.Category.FirstOrDefault(u => u.Id == item.CategoryId);
            }

            return View(gameList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CategoryDropDown = _db.Category.Select(i => new SelectListItem
            { 
                Text = i.Name,
                Value = i.Id.ToString()
            });

            ViewBag.CategoryDropDown = CategoryDropDown;

            return View();
        }
    }
}
