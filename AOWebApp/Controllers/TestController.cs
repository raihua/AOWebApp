using Microsoft.AspNetCore.Mvc;

namespace AOWebApp.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [HttpPost]
        public IActionResult RazorTest(int? id)
        {
            ViewBag.routeId = RouteData.Values["id"]?.ToString();

            if (Request.HasFormContentType)
            {
                ViewBag.formId = Request.Form["id"];
            }

            ViewBag.queryId = Request.Query["id"].FirstOrDefault();

            return View();
        }
    }
}
