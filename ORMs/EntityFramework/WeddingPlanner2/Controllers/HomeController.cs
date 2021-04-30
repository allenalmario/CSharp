using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WeddingPlanner2.Models;

namespace WeddingPlanner2.Controllers
{
    public class HomeController : Controller
    {
        private WeddingPlanner2Context db;
        public HomeController(WeddingPlanner2Context context)
        {
            db = context;
        }

        // Login Page
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View("Index");
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
                    Console.WriteLine(HttpContext.Session.GetInt32("LoggedUserId"));
                    return RedirectToAction("Dashboard");
                }
            }
            // if validations fail
            // return to show validaion errors
            return View("Index");
        }

        // Registration Page
        [HttpGet]
        [Route("/NewUser")]
        public IActionResult RegisterPage()
        {
            return View("Register");
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
                    return View("Register");
                }
            }
            // if validations do not pass
            if (ModelState.IsValid == false)
            {
                // return to log/reg page to
                // render the validation errors
                return View("Register");
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
            return RedirectToAction("Dashboard");
        }

        // Logout method
        [HttpPost]
        [Route("/Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("/Dashboard")]
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetInt32("LoggedUserId") == null)
            {
                return View("Index");
            }
            List<Wedding> allWeddings = db.Weddings.Include(w => w.Attendees).ToList();
            return View("Dashboard", allWeddings);
        }

        // new wedding page
        [HttpGet]
        [Route("/NewWeddingForm")]
        public IActionResult NewWeddingPage()
        {
            if (HttpContext.Session.GetInt32("LoggedUserId") == null)
            {
                return View("Index");
            }
            return View("WeddingForm");
        }

        // proccess new wedding
        [HttpPost]
        [Route("/NewWedding")]
        public IActionResult NewWedding(Wedding newWedding)
        {
            if (ModelState.IsValid)
            {
                if (newWedding.Date < DateTime.Now)
                {
                    ModelState.AddModelError("Date", "Date must be in the future");
                    return View("WeddingForm");
                }
                int userid = (int)HttpContext.Session.GetInt32("LoggedUserId");
                newWedding.UserId = userid;
                db.Weddings.Add(newWedding);
                db.SaveChanges();
                return RedirectToAction("Dashboard");

            }
            return View("WeddingForm");
        }

        [HttpGet]
        [Route("/Wedding/{weddingid}")]
        public IActionResult OneWedding(int weddingid)
        {
            Wedding thisWedding = db.Weddings
            .Include(w => w.Attendees)
            .ThenInclude(a => a.User)
            .FirstOrDefault(w => w.WeddingId == weddingid);
            return View("Wedding", thisWedding);
        }

        // RSVP METHOD
        [HttpPost]
        [Route("/RSVP/{weddingid}")]
        public IActionResult RSVP(int weddingid)
        {
            // is the user RSVP'd in the wedding coming in?
            UserWeddingRSVP existingRSVP = db.UserWeddingRSVPs
                .FirstOrDefault(rsvp => rsvp.UserId == (int)HttpContext.Session.GetInt32("LoggedUserId") && rsvp.WeddingId == weddingid);

            if (existingRSVP == null)
            {
                UserWeddingRSVP newRSVP = new UserWeddingRSVP()
                {
                    UserId = (int)HttpContext.Session.GetInt32("LoggedUserId"),
                    WeddingId = weddingid
                };
                db.UserWeddingRSVPs.Add(newRSVP);
            }
            else
            {
                db.UserWeddingRSVPs.Remove(existingRSVP);
            }
            db.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        //Delete Wedding
        [HttpPost]
        [Route("/Delete/{weddingid}")]
        public IActionResult Delete(int weddingid)
        {
            Wedding weddingToDelete = db.Weddings.FirstOrDefault(w => w.WeddingId == weddingid);
            db.Weddings.Remove(weddingToDelete);
            db.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        //update wedding page
        [HttpGet]
        [Route("/Edit/{weddingid}")]
        public IActionResult UpdatePage(int weddingid)
        {
            Wedding weddingToUpdate = db.Weddings.FirstOrDefault(w => w.WeddingId == weddingid);
            return View("UpdateWeddingForm", weddingToUpdate);
        }

        [HttpPost]
        [Route("/EditWedding/{weddingid}")]
        public IActionResult Edit(Wedding editedWedding, int weddingid)
        {
            if (ModelState.IsValid == false)
            {
                return View("UpdateWeddingForm", editedWedding);
            }
            Wedding weddingToEdit = db.Weddings.FirstOrDefault(w => w.WeddingId == weddingid);
            weddingToEdit.WedderOne = editedWedding.WedderOne;
            weddingToEdit.WedderTwo = editedWedding.WedderTwo;
            weddingToEdit.Date = editedWedding.Date;
            weddingToEdit.WeddingAddress = editedWedding.WeddingAddress;
            weddingToEdit.UpdatedAt = DateTime.Now;

            db.Weddings.Update(weddingToEdit);
            db.SaveChanges();
            return RedirectToAction("OneWedding", new{weddingid = weddingid});
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
