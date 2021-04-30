using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Crudelicious.Models;

namespace Crudelicious.Controllers
{
    public class HomeController : Controller
    {

        private CrudeliciousContext db;
        public HomeController(CrudeliciousContext context)
        {
            db = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {   
            // order by descending, in this case, newest first to oldest
            ViewBag.AllDishes = db.Dishes.OrderByDescending(d => d.CreatedAt).ToList();
            return View();
        }

        [HttpGet]
        [Route("/NewDish")]
        public IActionResult CreateDish()
        {
            return View("Create");
        }

        [HttpPost]
        [Route("/AddDish")]
        public IActionResult AddDish(Dish newDish)
        {
            if (ModelState.IsValid)
            {
                db.Add(newDish);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("Create");
            }
        }

        [HttpGet]
        [Route("/Dish/{dishId}")]
        public IActionResult singleDish(int dishId)
        {
            Dish singleDish = db.Dishes.FirstOrDefault(d => d.DishId == dishId);
            return View("SingleDish", singleDish);
        }

        [HttpPost]
        [Route("/Delete/{dishId}")]
        public IActionResult Delete(int dishId)
        {
            Dish dishToDelete = db.Dishes.FirstOrDefault(d => d.DishId == dishId);
            db.Dishes.Remove(dishToDelete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("/Edit/{dishId}")]
        public IActionResult Edit(int dishId)
        {
            Dish dishToEdit = db.Dishes.FirstOrDefault(d => d.DishId == dishId);
            return View("Edit", dishToEdit);
        }

        [HttpPost]
        [Route("/Update/dishId")]
        public IActionResult Update(Dish editedDish, int dishId)
        {
            if (ModelState.IsValid)
            {
                Dish updateDish = db.Dishes.FirstOrDefault(d => d.DishId == dishId);
                updateDish.Name = editedDish.Name;
                updateDish.Chef = editedDish.Chef;
                updateDish.Tastiness = editedDish.Tastiness;
                updateDish.Calories = editedDish.Calories;
                updateDish.Description = editedDish.Description;
                updateDish.UpdatedAt = DateTime.Now;

                db.Dishes.Update(updateDish);
                db.SaveChanges();

                return RedirectToAction("singleDish", new { dishId = dishId });

            }
            else
            {
                return View("Edit");
            }
        }

    }
}
