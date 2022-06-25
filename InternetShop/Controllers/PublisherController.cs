using InternetShop.Data;
using InternetShop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetShop.Controllers
{
    public class PublisherController : Controller
    {
        private readonly AppDbContext _db;

        public PublisherController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Publisher> publishers = _db.Publisher;
            return View(publishers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                _db.Publisher.Add(publisher);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(publisher);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            var publisher = _db.Publisher.Find(id);
            if (publisher == null)
            {
                return NotFound();
            }
            return View(publisher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                _db.Publisher.Update(publisher);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(publisher);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            var publisher = _db.Publisher.Find(id);
            if (publisher == null)
            {
                return NotFound();
            }
            return View(publisher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Publisher publisher)
        {
                _db.Publisher.Remove(publisher);
                _db.SaveChanges();
                return RedirectToAction("Index");
        }
    }
}
