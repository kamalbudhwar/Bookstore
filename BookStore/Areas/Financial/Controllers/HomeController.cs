using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Areas.Financial.Controllers
{
    public class HomeController : Controller
    {
        [Area("Financial")]
        [Route("financial/[controller]/[action]")]

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Welcome()
        {
            return View();
        }
    }
}
