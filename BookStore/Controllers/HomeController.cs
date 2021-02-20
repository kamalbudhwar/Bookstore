using BookStore.Models;
using BookStore.Repository;
using BookStore.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        // private readonly NewBookAlertConfig _newBookAlertConfiguration;
        // private readonly IMessageRepository _messageRepository;
        //IOptionsSnapshot<NewBookAlertConfig> newBookAlertConfig,IMessageRepository msgrepo
        public HomeController(IUserService userService,IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;

            //_newBookAlertConfiguration = newBookAlertConfig.Value;
            //_messageRepository = msgrepo;

        }
        [ViewData]
        public String Title { get; set; }
        public async Task<ViewResult> Index()
         {
            //var isDisplay = _newBookAlertConfiguration;
            // var Result = _configuration["AppName"];
            //var msg = _messageRepository.GetName();

        //    UserEmailOptions userEmailOptions = new UserEmailOptions
         //   {
         //       ToEmails = new List<string> { "test@gmail.com" },
         //       Placeholders = new List<KeyValuePair<string, string>>()
         //       {
         //           new KeyValuePair<string, string>("{{Guest}}","Pinku"){ }
         //       }
                
         //   };
          //  await _emailService.SendTestEmail(userEmailOptions);
             // var userId = _userService.GetUserId();
            //var isLoogedIn = _userService.isAuthenticated();
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
