using AOWebApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace AOWebApp.Controllers
{
    public class ReportsController : Controller
    {
        private readonly AmazonOrdersContext _context;

        public ReportsController(AmazonOrdersContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View("AnnualSalesReport");
        }
    }
}
