using Microsoft.AspNetCore.Mvc;

namespace EasyHousingSolutionMVC.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {

            //ViewBag.Message = $"Welcome Admin ";
            ViewBag.Message = HttpContext.Session.GetString("Role");
            return View();
        }
        public IActionResult Admin()
        {
            return View();
        }
    }
}
