using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ViewModelFun.Models;

namespace ViewModelFun.Controllers
{
    public class FunController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            string paragraph = "Hello World! I am currently playing with ViewModels in the ViewModel Fun assignment for my Coding Bootcamp Coding Dojo! Super Fun!!!!!!";
            return View("index", paragraph);
        }

        [HttpGet]
        [Route("numbers")]
        public IActionResult Numbers()
        {
            int[] numArr = new int[]
            {
                1, 2, 3, 10, 43, 5
            };
            return View("numbers", numArr);
        }

        [HttpGet]
        [Route("users")]
        public IActionResult Users()
        {
            User user1 = new User{
                Name = "Moose Phillips"
            };
            User user2 = new User{
                Name = "Sarah"
            };
            User user3 = new User{
                Name = "Jerry"
            };
            User user4 = new User{
                Name = "Rene Ricky"
            };
            User user5 = new User{
                Name = "Barbarah"
            };
            List<User> users = new List<User>(){
                user1, user2, user3, user4, user5
            };
            return View("Users", users);
        }

        [HttpGet]
        [Route("user")]
        public IActionResult User()
        {
            User CoolUser = new User(){
                Name = "John Johnson"
            };
            return View("user", CoolUser);
        }
    }
}