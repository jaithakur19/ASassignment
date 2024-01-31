using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor contxt;

        public HomeController(IHttpContextAccessor httpContextAccessor)
        {
            contxt = httpContextAccessor;
        }

        public IActionResult Index()
        {
            contxt.HttpContext.Session.SetString("StudentName", "Tim");
            contxt.HttpContext.Session.SetInt32("StudentId", 50);

            return View();
        }

        public IActionResult Privacy()
        {
            string StudentName = contxt.HttpContext.Session.GetString("StudentName");
            return View();
        }
    }

}
