using InternetShop.Data;
using InternetShop.Models;
using InternetShop.Models.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace InternetShop.Controllers
{
    public class GameController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public GameController(AppDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnviroment = webHostEnvironment;
        }

        private IEnumerable<SelectListItem> GetCategorySelectList()
        {
            var categorySelectList = _db.Category.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            return categorySelectList;
        }

        private IEnumerable<SelectListItem> GetPublisherSelectList()
        {
            var publisherSelectList = _db.Publisher.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            return publisherSelectList;
        }

        public IActionResult Index()
        {
            IEnumerable<Game> gameList = _db.Game.Include(u => u.Category).Include(u => u.Publisher);

            return View(gameList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CategoryDropDown = GetCategorySelectList();
            ViewBag.PublisherDropDown = GetPublisherSelectList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Game game)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string upload = _webHostEnviroment.WebRootPath + WebConst.ImagePath;
                string fileName = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                game.Image = fileName + extension;
                _db.Game.Add(game);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.CategoryDropDown = GetCategorySelectList();
            ViewBag.PublisherDropDown = GetPublisherSelectList();
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            GameVM gameVM = new GameVM()
            {
                Game = new Game(),
                CategorySelectList = GetCategorySelectList(),
                PublisherSelectList = GetPublisherSelectList()
            };

            gameVM.Game = _db.Game.Find(id);
            if (gameVM.Game == null)
            {
                return NotFound();
            }

            return View(gameVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(GameVM gameVM)
        {
            if (!ModelState.IsValid)
            {
                gameVM.CategorySelectList = GetCategorySelectList();
                gameVM.PublisherSelectList = GetPublisherSelectList();
                return View(gameVM);
            }

            /*
                Метод AsNoTraking для отключения отслеживания объекта
                ASP.NET, при вызове метода update не возникало коллизий
             */
            var gameFromDb = _db.Game.AsNoTracking().FirstOrDefault(u => u.Id == gameVM.Game.Id);
            
            var files = HttpContext.Request.Form.Files;

            if (files.Count > 0)
            {

                string upload = _webHostEnviroment.WebRootPath + WebConst.ImagePath;
                string fileName = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(files[0].FileName);

                string oldFile = Path.Combine(upload, gameFromDb.Image);

                if (System.IO.File.Exists(oldFile))
                {
                    System.IO.File.Delete(oldFile);
                }

                using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                gameVM.Game.Image = fileName + extension;
            }
            else
            {
                gameVM.Game.Image = gameFromDb.Image;
            }

            _db.Game.Update(gameVM.Game);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            GameVM gameVM = new GameVM()
            {
                Game = new Game(),
                CategorySelectList = GetCategorySelectList(),
                PublisherSelectList = GetPublisherSelectList()
            };

            gameVM.Game = _db.Game.Find(id);
            if (gameVM.Game == null)
            {
                return NotFound();
            }

            return View(gameVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(GameVM gameVM)
        {
            var gameFromDb = _db.Game.AsNoTracking().FirstOrDefault(u => u.Id == gameVM.Game.Id);
            string upload = _webHostEnviroment.WebRootPath + WebConst.ImagePath;
            string oldFile = Path.Combine(upload, gameFromDb.Image);

            if (System.IO.File.Exists(oldFile))
            {
                System.IO.File.Delete(oldFile);
            }

            _db.Remove(gameVM.Game);
            _db.SaveChanges();

            return RedirectToAction("Index");

        }
        
    }
}
