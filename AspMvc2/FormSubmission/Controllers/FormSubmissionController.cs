using Microsoft.AspNetCore.Mvc;
using FormSubmission.Models;
namespace FormSubmission.Controllers
{
    public class FormSubmissionController : Controller
    {
        [HttpGet]
        [Route("")]
        public ViewResult Index()
        {
            return View("index");
        }

        [HttpGet]
        [Route("Success")]
        public ViewResult Success()
        {
            return View("success");
        }

        [HttpPost]
        [Route("CreateUser")]
        public IActionResult Create(User user)
        {
            if(ModelState.IsValid)
            {
                return RedirectToAction("Success");
            }
            else
            {
                return View("index");
            }
        }
    }
}