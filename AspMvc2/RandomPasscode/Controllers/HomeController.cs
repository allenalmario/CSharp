using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RandomPasscode.Models;
using Microsoft.AspNetCore.Http;

namespace RandomPasscode.Controllers
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
            if (HttpContext.Session.GetInt32("count") == null)
            {
                HttpContext.Session.SetInt32("count", 1);
            }
            else
            {
                int? currentCount = HttpContext.Session.GetInt32("count");
                currentCount++;
                HttpContext.Session.SetInt32("count", (int)currentCount);
            }
            ViewBag.Count = HttpContext.Session.GetInt32("count");
            Random rand = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var randChars = new char[14];
            for (int i = 0; i < randChars.Length; i++)
            {
                randChars[i] = chars[rand.Next(0, 36)];
            }
            string generatedPassword = new String(randChars);
            ViewBag.password = generatedPassword;
            return View("Index");
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
