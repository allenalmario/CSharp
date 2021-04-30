using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dojodachi.Models;
using Microsoft.AspNetCore.Http;

namespace Dojodachi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            int? full = HttpContext.Session.GetInt32("fullness");
            if (full == null)
            {
                HttpContext.Session.SetInt32("fullness", 20);
            }

            int? happy = HttpContext.Session.GetInt32("happiness");
            if (happy == null)
            {
                HttpContext.Session.SetInt32("happiness", 20);
            }

            int? energy = HttpContext.Session.GetInt32("energy");
            if (energy == null)
            {
                HttpContext.Session.SetInt32("energy", 50);
            }

            int? meals = HttpContext.Session.GetInt32("meals");
            if (meals == null)
            {
                HttpContext.Session.SetInt32("meals", 3);
            }

            string message = HttpContext.Session.GetString("message");
            if (message == null)
            {
                HttpContext.Session.SetString("message", "");
            }

            ViewBag.Happiness = HttpContext.Session.GetInt32("happiness");
            ViewBag.Fullness = HttpContext.Session.GetInt32("fullness");
            ViewBag.Energy = HttpContext.Session.GetInt32("energy");
            ViewBag.Meals = HttpContext.Session.GetInt32("meals");
            ViewBag.Message = HttpContext.Session.GetString("message");

            return View("Index");
        }

        [HttpPost]
        [Route("Feed")]
        public IActionResult Feed()
        {
            Random rand = new Random();
            int chances = rand.Next(1,5);
            int? currentMeals = HttpContext.Session.GetInt32("meals");
            if (chances != 1)
            {
                currentMeals--;
                HttpContext.Session.SetInt32("meals", (int)currentMeals);
                int randFull = rand.Next(5, 11);
                int? currentFull = HttpContext.Session.GetInt32("fullness");
                currentFull += randFull;
                HttpContext.Session.SetInt32("fullness", (int)currentFull);
                string customMessage = $"You fed your Dojodachi! Fullness +{randFull}, Meals -1";
                HttpContext.Session.SetString("message", customMessage);
            }
            else if (chances == 1)
            {
                currentMeals--;
                HttpContext.Session.SetInt32("meals", (int)currentMeals);
                string customMessage = $"You fed your Dojodachi! However, they didn't like it. Fullness didn't go up, Meals -1";
                HttpContext.Session.SetString("message", customMessage);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("Play")]
        public IActionResult Play()
        {
            Random rand = new Random();
            int chances = rand.Next(1,5);
            int? currentEnergy = HttpContext.Session.GetInt32("energy");
            if (chances != 1)
            {
                currentEnergy = currentEnergy - 5;
                HttpContext.Session.SetInt32("energy", (int)currentEnergy);
                int randHappy = rand.Next(5, 11);
                int? currentHappy = HttpContext.Session.GetInt32("happiness");
                currentHappy += randHappy;
                HttpContext.Session.SetInt32("happiness", (int)currentHappy);
                string customMessage = $"You played with your Dojodachi! Energy -5, Happiness +{randHappy}";
                HttpContext.Session.SetString("message", customMessage);
            }
            else if (chances == 1)
            {
                currentEnergy = currentEnergy - 5;
                HttpContext.Session.SetInt32("energy", (int)currentEnergy);
                string customMessage = $"You played with your Dojodachi! However, they didn't like it. Happiness didn't go up, Energy -5";
                HttpContext.Session.SetString("message", customMessage);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("Work")]
        public IActionResult Work()
        {
            Random rand = new Random();
            int randMeals = rand.Next(1,4);
            int? currentEnergy = HttpContext.Session.GetInt32("energy");
            currentEnergy = currentEnergy - 5;
            HttpContext.Session.SetInt32("energy", (int)currentEnergy);
            int? currentMeals = HttpContext.Session.GetInt32("meals");
            currentMeals += randMeals;
            HttpContext.Session.SetInt32("meals", (int)currentMeals);
            string customMessage = $"Your Dojodachi worked! Energy -5, Meals +{randMeals}";
            HttpContext.Session.SetString("message", customMessage);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("Sleep")]
        public IActionResult Sleep()
        {
            int? currentFull = HttpContext.Session.GetInt32("fullness");
            int? currentHappy = HttpContext.Session.GetInt32("happiness");
            int? currentEnergy = HttpContext.Session.GetInt32("energy");
            currentEnergy = currentEnergy + 15;
            HttpContext.Session.SetInt32("energy", (int)currentEnergy);
            currentFull -= 5;
            currentHappy -= 5;
            HttpContext.Session.SetInt32("fullness", (int)currentFull);
            HttpContext.Session.SetInt32("happiness", (int)currentHappy);
            string customMessage = $"Your Dojodachi went to sleep! Energy +15, Fullness -5, Happiness -5";
            HttpContext.Session.SetString("message", customMessage);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
