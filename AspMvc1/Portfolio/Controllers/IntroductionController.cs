using Microsoft.AspNetCore.Mvc;
namespace Portfolio.Controllers
{
    public class IntroController : Controller
    {
        // Handles Request
        // Routing method is Attribute Routing
        [HttpGet] // Http Method Attribute
        [Route("")] // Route Attribute

        //Handles Response
        //The method below is called an Action
        // This method delivers a server response
        public ViewResult Index(){
            return View("index");
        }

        [HttpGet]
        [Route("projects")]
        public ViewResult Projects()
        {
            return View("projects");
        }


        [HttpGet]
        [Route("contact")]
        public ViewResult Contact()
        {
            return View("contact");
        }

    }
}