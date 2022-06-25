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
    }
}
