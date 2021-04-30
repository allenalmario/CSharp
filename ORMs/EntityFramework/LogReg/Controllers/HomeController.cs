using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LogReg.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace LogReg.Controllers
{
    public class HomeController : Controller
    {

        private LogRegContext db;
        public HomeController(LogRegContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Register Method
        [HttpPost]
        [Route("/register")]
        public IActionResult Register(User newUser)
        {
            // If Registration succeeds
            if (ModelState.IsValid)
            {
                // Check if email already exists in database
                if (db.Users.Any(u => u.Email == newUser.Email))
                {
                    // typically don't want to be descriptive, but be ambigious about this validation
                    // only being descriptive for development purposes
                    ModelState.AddModelError("Email", "is already in use!");
                }
            }
            // check if its valid again b/c it may have been changed above
            if (ModelState.IsValid == false)
            {
                return View("Index");
            }
            // if code reaches here, means it passed validations
            // before adding user to DB, need to hash the password
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
            // now save
            db.Users.Add(newUser);
            db.SaveChanges();
            HttpContext.Session.SetInt32("UserId", newUser.UserId); // set session with user's ID
            return RedirectToAction("Success"); // redirect them to "Home" page
        }

        // Login Method
        [HttpPost]
        [Route("/login")]
        public IActionResult Login(LoginUser loginuser)
        {

            string genericErrorMsg = "Invalid Email or Password";
            // if login form passes validations
            if (ModelState.IsValid)
            {
                var userInDb = db.Users.FirstOrDefault(u => u.Email == loginuser.LoginEmail);
                // if no user is returned
                if (userInDb == null)
                {
                    // Add error
                    ModelState.AddModelError("LoginEmail", genericErrorMsg);
                    return View("Index");
                }
                // initialize hasher object
                var hasher = new PasswordHasher<LoginUser>();
                // verify password against hash stored in db
                var result = hasher.VerifyHashedPassword(loginuser, userInDb.Password, loginuser.LoginPassword);
                // result can be compared to 0 for failure
                if(result == 0)
                {
                    ModelState.AddModelError("LoginPassword", genericErrorMsg);
                    return View("Index");
                }
                // if reaches here, means passed
                HttpContext.Session.SetInt32("UserId", userInDb.UserId); // set session with user's ID
                HttpContext.Session.SetString("UserName", userInDb.FirstName);
                return RedirectToAction("Success");
            }
            return View("Index");
        }

        [HttpGet]
        [Route("/success")]
        public IActionResult Success()
        {
            int? sessionId = HttpContext.Session.GetInt32("UserId");
            if (sessionId == null)
            {
                return View("Index");
            }
            return View("Success");
        }

        [HttpPost]
        [Route("/logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }
    }
}
