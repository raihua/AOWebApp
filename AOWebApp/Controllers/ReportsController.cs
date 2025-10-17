using AOWebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

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
            var distinctYears = _context.CustomerOrders
                .Select(co => co.OrderDate.Year)
                .Distinct()
                .OrderByDescending(co => co)
                .ToList();

            return View("AnnualSalesReport", new SelectList(distinctYears));
        }

        [Produces("application/json")]
        public IActionResult AnnualSalesReportData(int year)
        {
            if (year > 0)
            {
                var numItemsSold = _context.ItemsInOrders
                    .Join(_context.CustomerOrders,
                    iio => iio.OrderNumber,
                    co => co.OrderNumber,
                    (iio, co) => new { ItemsInOrder = iio, CustomerOrders = co })
                    .Where(record => record.CustomerOrders.OrderDate.Year == year)
                    .GroupBy(record => new
                    {
                        year = record.CustomerOrders.OrderDate.Year,
                        month = record.CustomerOrders.OrderDate.Month,
                    })
                    .Select(orderGroup => new
                    {
                        year = orderGroup.Key,
                        monthNo = orderGroup.Key.month,
                        monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(orderGroup.Key.month),
                        totalItems = orderGroup.Sum(orderGroup => orderGroup.ItemsInOrder.NumberOf),
                        totalSales = orderGroup.Sum(orderGroup => orderGroup.ItemsInOrder.TotalItemCost)
                    })
                    .OrderBy(record => record.monthNo)
                    .ToList(); 

                return Json(numItemsSold);
            }
            else
            {
                return BadRequest();
            }
        }
    }

}
