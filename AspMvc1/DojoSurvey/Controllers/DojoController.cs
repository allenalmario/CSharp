using Microsoft.AspNetCore.Mvc;
using DojoSurvey.Models;
namespace DojoSurvey.Controllers
{
    public class DojoController : Controller
    {
        [HttpGet]
        [Route("")]
        public ViewResult Index()
        {
            return View("index");
        }

        [HttpPost]
        [Route("Submit")]
        public IActionResult Process(DojoSubmission response)
        {
            if(ModelState.IsValid)
            {
                return View("result", response);
            }
            else
            {
                return View("index");
            }
        }
    }
}