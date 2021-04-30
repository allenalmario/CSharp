using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingPlanner.Models;

namespace WeddingPlanner.Controllers
{
    public class WeddingController : Controller
    {
        private WeddingPlannerContext db;
        public WeddingController(WeddingPlannerContext context)
        {
            db = context;
        }
        
        // Render the dashboard
        [HttpGet]
        [Route("/Dashboard")]
        public IActionResult Dashboard()
        {
            // check to see if a user is in session
            int? LoggedUser = HttpContext.Session.GetInt32("LoggedUserId");
            // means no user in session
            // redirect to Login/Reg page
            if (LoggedUser == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // gather all weddings from Weddings DB
            List<Wedding> allWeddings = db.Weddings.Include(w => w.Creator).Include(u => u.Guests).ToList();
            return View("Dashboard", allWeddings);
        }

        // Render individual Wedding
        [HttpGet]
        [Route("/Wedding/{weddingid}")]
        public IActionResult Wedding(int weddingid)
        {
            // check to see if a user is in session
            int? LoggedUser = HttpContext.Session.GetInt32("LoggedUserId");
            // means no user in session
            // redirect to Login/Reg page
            if (LoggedUser == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // get the wedding object using the weddingid parameter
            Wedding oneWedding = db.Weddings.Include(w => w.Creator).Include(w => w.Guests).ThenInclude(u => u.User).FirstOrDefault(w => w.WeddingId == weddingid);
            return View("Wedding", oneWedding);
        }

        // Render the Add Wedding Form page
        [HttpGet]
        [Route("/New-Wedding")]
        public IActionResult NewWedding()
        {
            // check to see if a user is in session
            int? LoggedUser = HttpContext.Session.GetInt32("LoggedUserId");
            // means no user in session
            // redirect to Login/Reg page
            if (LoggedUser == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("WeddingForm");
        }

        // Adding the Wedding to DB
        [HttpPost]
        [Route("/Add-Wedding")]
        public IActionResult AddWedding(Wedding newWedding)
        {
            // if passes validations
            if (ModelState.IsValid)
            {
                // see if the wedding already exists in the DB
                if(db.Weddings.Any(w => w.WedderOne == newWedding.WedderOne && w.WedderTwo == newWedding.WedderTwo))
                {
                    ModelState.AddModelError("WedderOne", "Wedding already exists!");
                    return View("WeddingForm");
                }
                // check to see if wedding date is in the future, if not, return to form to show error
                if(!(DateTime.Now < newWedding.Date))
                {
                    ModelState.AddModelError("Date", "Date must be in the future");
                    return View("WeddingForm");
                }
                // if the wedding doesn't exist,
                // then add it to the DB

                // need to associate the UserId Foriegn Key
                // in newWedding Object to userid making wedding
                newWedding.UserId = (int)HttpContext.Session.GetInt32("LoggedUserId");
                db.Weddings.Add(newWedding);
                db.SaveChanges();
                // send the newWedding Id to the individual wedding page
                return RedirectToAction("Wedding", new{weddingid = newWedding.WeddingId});
            }
            // if does not pass validations
            // Return to form and show validation errors
            return View("WeddingForm");
        }

        // Edit a 
        [HttpGet]
        [Route("/Edit/{weddingid}")]
        public IActionResult Edit(int weddingid)
        {
            Wedding WeddingToEdit = db.Weddings.FirstOrDefault(w => w.WeddingId == weddingid);
            return View("EditWedding", WeddingToEdit);
        }

        // Process Wedding Edit
        [HttpPost]
        [Route("/UpdateWedding/{weddingid}")]
        public IActionResult UpdateWedding(Wedding editedWedding, int weddingid)
        {
            // check validations
            if (ModelState.IsValid)
            {
                Wedding updateWedding = db.Weddings.FirstOrDefault(w => w.WeddingId == weddingid);
                updateWedding.WedderOne = editedWedding.WedderOne;
                updateWedding.WedderTwo = editedWedding.WedderTwo;
                updateWedding.Date = editedWedding.Date;
                updateWedding.WeddingAddress = editedWedding.WeddingAddress;
                db.Weddings.Update(updateWedding);
                db.SaveChanges();
                return RedirectToAction("Wedding", new{weddingid = weddingid});
            }
            // if validations fail
            return View("EditWedding");
        }

        // RSVP/Un-RSVP Method
        [HttpPost]
        [Route("/RSVP/{weddingid}")]
        public IActionResult RSVP(int weddingid)
        {
            // Want to see if the user is RSVP'd for the specific wedding coming in
            UserWeddingGuest existingRSVP = db.UserWeddingGuests
                .FirstOrDefault(rsvp => rsvp.UserId == (int)HttpContext.Session.GetInt32("LoggedUserId") && rsvp.WeddingId == weddingid);

            // if RSVP is clicked, this is supposed to be null
            // because The user is not RSVP'd to the specific wedding
            // so RSVP the user to the specific wedding
            if (existingRSVP == null)
            {
                // Normally model is instantiated for us in the params of the
            // method, but because the page this form is on already has its own
            // model, we will instantiate it ourselves. Otherwise, we could put
            // the form into a <partial> view to giove it a @model so it would be
            // auto instantiated
            UserWeddingGuest newRSVP = new UserWeddingGuest()
            {
                UserId = (int)HttpContext.Session.GetInt32("LoggedUserId"),
                WeddingId = weddingid
            };
            // add new RSVP to the DB
            db.UserWeddingGuests.Add(newRSVP);
            }

            // if user clicks Un-RSVP, means they were already in the DB
            // RSVP'd to this specific wedding, so comes back a UserWeddingGuest
            // object, then want to remove this instance that combines the two
            // models to remove the user from weddings Guest list and wedding from User Weddings list
            else
            {
                db.UserWeddingGuests.Remove(existingRSVP);
            }
            // save changes in DB
            db.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        //Delete method
        [HttpPost]
        [Route("/Delete/{weddingid}")]
        public IActionResult Delete(int weddingid)
        {
            // get the instance to delete from the db
            Wedding WeddingToDelete = db.Weddings.FirstOrDefault(w => w.WeddingId == weddingid);
            // then delete instance from DB
            db.Weddings.Remove(WeddingToDelete);
            db.SaveChanges();
            return RedirectToAction("Dashboard");
        }
    }
}