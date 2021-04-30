using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ChefsNDishes.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefsNDishes.Controllers
{
    public class HomeController : Controller
    {
        private ChefsNDishesContext db;

        public HomeController(ChefsNDishesContext context)
        {
            db = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            // gather all dishes
            // order newest to oldest added
            List<Dish> allDishes = db.Dishes.Include(d => d.Creator).OrderByDescending(d => d.CreatedAt).ToList();
            ViewBag.Dishes = allDishes;
            return View("Index");
        }

        [HttpGet]
        [Route("/chefs")]
        public IActionResult Chefs()
        {
            // gather all chefs
            // order newest to oldest added
            List<Chef> allChefs = db.Chefs.Include(c => c.CreatedDishes).OrderByDescending(c => c.CreatedAt).ToList();
            ViewBag.Chefs = allChefs;
            return View("Chefs", allChefs);
        }

        [HttpGet]
        [Route("/create_chef")]
        public IActionResult CreateChef()
        {
            return View("CreateChef");
        }

        [HttpPost]
        [Route("/add_chef")]
        public IActionResult AddChef(Chef newChef)
        {
            if (ModelState.IsValid)
            {
                db.Add(newChef);
                db.SaveChanges();
                return RedirectToAction("Chefs");
            }
            // if validations do not pass, go back to form page
            // to display validation errors
            return View("CreateChef");
        }

        [HttpGet]
        [Route("/create_dish")]
        public IActionResult CreateDish()
        {
            // gather all chefs
            List<Chef> allChefs = db.Chefs.ToList();
            ViewBag.Chefs = allChefs;
            return View("CreateDish");
        }

        [HttpPost]
        [Route("/add_dish")]
        public IActionResult AddDish(Dish newDish)
        {
            if (ModelState.IsValid)
            {
                db.Add(newDish);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // if validations do not pass, go back to form page
            // to display validation errors
            // gather all chefs
            List<Chef> allChefs = db.Chefs.ToList();
            ViewBag.Chefs = allChefs;
            return View("CreateDish");
        }
    }
}
