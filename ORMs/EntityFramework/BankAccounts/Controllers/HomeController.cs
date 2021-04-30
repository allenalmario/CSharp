using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BankAccounts.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BankAccounts.Controllers
{
    public class HomeController : Controller
    {
        private BankAccountsContext db;

        public HomeController(BankAccountsContext context)
        {
            db = context;
        }

        // Home page
        [HttpGet]
        [Route("/account/{userId}")]
        public IActionResult Index(int userId)
        {
            // check to see if a user is logged in session
            int? sessionId = HttpContext.Session.GetInt32("UserId");
            // if nothing comes back, login/reg was unsuccessful
            // return to login page
            if (sessionId != userId)
            {
                return RedirectToAction("Index", new {userId = sessionId});
            }
            User thisUser = db.Users.FirstOrDefault(u => u.UserId == (int)sessionId);
            List<Transaction> UserTransactions = db.Transactions.Include(u => u.BankCustomer).Where(t => t.BankCustomer.UserId == (int)sessionId).OrderByDescending(d => d.CreatedAt).ToList();
            ViewBag.User = thisUser;
            ViewBag.Transactions = UserTransactions;
            return View("Index");
            
        }

        // Register page
        [HttpGet]
        [Route("/new-user")]
        public IActionResult RegisterPage()
        {
            return View("Register");
        }

        //Register user method
        [HttpPost]
        [Route("/register")]
        public IActionResult Register(User newUser)
        {
            // if Registration validations pass
            if (ModelState.IsValid)
            {
                // check to see if email already exists in DB,
                // if so
                if(db.Users.Any(u => u.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "is already in use!");
                }
            }
            // check if it's valid again b/c it may have been changed above
            if (ModelState.IsValid)
            {
                // assuming registration validations passes
                // want to hash the password before user info put in DB
                PasswordHasher<User> hasher = new PasswordHasher<User>();
                newUser.Password = hasher.HashPassword(newUser, newUser.Password);
                // then save newUser object to DB
                db.Users.Add(newUser);
                db.SaveChanges();
                // set session to user's id
                // and redirecttoaction home page
                HttpContext.Session.SetInt32("UserId", newUser.UserId);
                return RedirectToAction("Index", new{userId = newUser.UserId});
            }
            // if both these code blocks fail, validations failed
            // return back to register page to show validation errors
            return View("Register");
        }

        //Login Page
        [HttpGet]
        [Route("/login")]
        public IActionResult LoginPage()
        {
            return View("Login");
        }

        // login user method
        [HttpPost]
        [Route("/loginsuccess")]
        public IActionResult Login(LoginUser loginuser)
        {
            if (ModelState.IsValid)
            {
                // check to see if user currently exists in DB
                User userInDB = db.Users.FirstOrDefault(u => u.Email == loginuser.LoginEmail);
                if (userInDB == null)
                {
                    // no user was found with the loginemail provided
                    ModelState.AddModelError("LoginEmail", "Invalid Email or Password");
                    return View("Login");
                }
                // email exists, meaning user is in db
                // now compare the hashed loginpassword to the hashed one in the DB
                // first create a password hasher object
                PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
                // compare povided pw to the hashed pw in db using
                // VerifyHashedPassword method, returns 1 or 0, success or failure
                var result = hasher.VerifyHashedPassword(loginuser, userInDB.Password, loginuser.LoginPassword);
                // verification failed
                if (result == 0)
                {
                    ModelState.AddModelError("LoginPassword", "Invalid Email/Password");
                    return View("Login");
                }
                HttpContext.Session.SetInt32("UserId", userInDB.UserId);
                HttpContext.Session.SetString("FullName", userInDB.FirstName + " " + userInDB.LastName);
                return RedirectToAction("Index", new{userId = userInDB.UserId});
            }
            // if validations failed
            // return to login page to display validation errors
            return View("Login");
        }

        [HttpPost]
        [Route("/transaction/{userid}")]
        public IActionResult Transaction(Transaction newTransaction, int userid)
        {
            int? sessionId = HttpContext.Session.GetInt32("UserId");
            User thisUser = db.Users.FirstOrDefault(u => u.UserId == (int)sessionId);
            Console.WriteLine(newTransaction.Amount);
            if (newTransaction.Amount > thisUser.CurrentBalance)
            {
                Console.WriteLine("Getting to Method");
                ModelState.AddModelError("Amount", "Cannot withdraw more than your current balance");
                return View("Index", new{userId = sessionId});
            }
            if (newTransaction.Amount == 0)
            {
                ModelState.AddModelError("Amount", "Amount cannot be 0!");
                return View("Index", new{userId = sessionId});
            }
            // Deposit
            if (newTransaction.Amount > 0)
            {
                thisUser.CurrentBalance = thisUser.CurrentBalance + newTransaction.Amount;
            }
            // Withdraw ( minus a negative, have to add or else, two negative cancels each other out)
            else if (newTransaction.Amount < 0)
            {
                thisUser.CurrentBalance = thisUser.CurrentBalance + newTransaction.Amount;
            }
            db.Transactions.Add(newTransaction);
            db.SaveChanges();
            return RedirectToAction("Index", new {userId = HttpContext.Session.GetInt32("UserId")});
        }
        [HttpPost]
        [Route("/logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LoginPage");
        }
    }
}
