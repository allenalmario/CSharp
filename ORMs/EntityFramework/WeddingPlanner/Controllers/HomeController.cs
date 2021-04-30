using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeddingPlanner.Models;

namespace WeddingPlanner.Controllers
{
    public class HomeController : Controller
    {
        private WeddingPlannerContext db;
        public HomeController(WeddingPlannerContext context)
        {
            db = context;
        }

        // LOG AND REG PAGE
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        // REGISTRATION METHOD
        [HttpPost]
        [Route("/Register")]
        public IActionResult Register(User newUser)
        {
            // If passed attribute validations
            if (ModelState.IsValid)
            {
                // check to see if Email already
                // exists in the DB
                if (db.Users.Any(u => u.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "Information invalid");
                    return View("Index");
                }
            }
            // if validations do not pass
            if (ModelState.IsValid == false)
            {
                // return to log/reg page to
                // render the validation errors
                return View("Index");
            }
            // if pass all validations
            // first hash the password
            // before saving user info
            // to the DB

            // create a password hasher object
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            newUser.Password = Hasher.HashPassword(newUser, newUser.Password); // hashing method required an instance of
            // the type to be passed to it

            // finally, save newUser to DB
            db.Users.Add(newUser);
            db.SaveChanges();
            // save to session
            HttpContext.Session.SetInt32("LoggedUserId", newUser.UserId);
            return RedirectToAction("Dashboard", "Wedding");
        }

        // LOGIN METHOD
        [HttpPost]
        [Route("/Login")]
        public IActionResult Login(LoginUser loginuser)
        {
            if (ModelState.IsValid)
            {
                //check to see if user exists in the DB
                User userInDb = db.Users.FirstOrDefault(u => u.Email == loginuser.LoginEmail);
                // if nothing comes back, means that user does not exist in the DB
                if (userInDb == null)
                {
                    ModelState.AddModelError("LoginEmail", "Invalid login information");
                    return View("Index");
                }
                if (ModelState.IsValid)
                {
                    // initialize hasher object
                    PasswordHasher<LoginUser> Hasher = new PasswordHasher<LoginUser>();
                    PasswordVerificationResult verificationresult = Hasher.VerifyHashedPassword(loginuser, userInDb.Password, loginuser.LoginPassword);
                    if (verificationresult == 0)
                    {
                        ModelState.AddModelError("LoginEmail", "Invalid login information");
                        // return login page to display errors
                        return View("Index");
                    }
                    // if login is successful
                    // save user in session
                    HttpContext.Session.SetInt32("LoggedUserId", userInDb.UserId);
                    return RedirectToAction("Dashboard", "Wedding");
                }
            }
            // if validations fail
            // return to show validaion errors
            return View("Index");
        }

        // Logout method
        [HttpPost]
        [Route("/Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
