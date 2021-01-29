using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        [ViewData]
        public String Title { get; set; }
        public ViewResult Index()
        {
            Title = "Home";
            return View();
        }
        public ViewResult AboutUs()
        {
            Title = "About Us";
            return View();
        }
        public ViewResult ContactUs()
        {
            Title = "Contact Us";
            return View();
        }

    }
}
