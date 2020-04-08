using Microsoft.AspNetCore.Mvc;

namespace ChartDemo.Controllers
{
    public class ChartController : Controller
    {
        public IActionResult Area()
        {
            return View();
        }
        public IActionResult Pie()
        {
            return View();
        }
        public IActionResult Scatter()
        {
            return View();
        }
    }
}