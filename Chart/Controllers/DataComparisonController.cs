using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ChartDemo.Controllers
{
    public class DataComparisonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}