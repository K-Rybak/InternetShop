using InternetShop.Data;
using InternetShop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _db;

        public CategoryController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> categoryList = _db.Category;
            return View(categoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category value)
        {
            if (ModelState.IsValid)
            {
                _db.Category.Add(value);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(value);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id <= 0){
                return NotFound();
            }

            var category = _db.Category.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category value)
        {
            if (ModelState.IsValid)
            {
                _db.Category.Update(value);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(value);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            var category = _db.Category.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category value)
        {
                _db.Category.Remove(value);
                _db.SaveChanges();
                return RedirectToAction("Index");
        }
    }
}
