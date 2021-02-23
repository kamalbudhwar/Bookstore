using BookStore.Models;
using BookStore.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public AccountRepository(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,IUserService userService,IEmailService emailService,IConfiguration configuration)
        {
           _userManager = userManager;
           _signInManager = signInManager;
           _userService = userService;
           _emailService = emailService;
            _configuration = configuration;
        } 
        public async Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel)
        {
            var user = new ApplicationUser()
            {
                Email = userModel.Email,
                UserName = userModel.Email,
                FirstName=userModel.FirstName,
                LastName=userModel.LastName,
                DateOfBirth=userModel.DateOfBirth
            };

            var result=await _userManager.CreateAsync(user, userModel.Password);
            if (result.Succeeded)
            {
                await GenerateTokenToConfirmEmailAsync(user);
                userModel.sentConfirmMail = true;
            }
            
            return result;
        }

        public async Task<ApplicationUser> GetUserByEmailId(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task GenerateTokenToConfirmEmailAsync(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            if (token != null)
            {
                await SendEmailConfirmationEmail(user, token);
            }
        }

        public async Task GenerateTokenForForgetPasswordAsync(ApplicationUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (token != null)
            {
                await SendForgotPasswordEmail(user, token);
            }
        }


        public async Task<SignInResult> PasswordSignInAsync(SignInModel signInModel)
        {
            return await _signInManager.PasswordSignInAsync(signInModel.Email,signInModel.Password,signInModel.RememberMe,true);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();

        }

        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel model)
        {
            var userId =_userService.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        }

        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            return await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
        }


        public async Task<IdentityResult> ConfirmEmailAsync(string uid, string token)
        {
            var user = await _userManager.FindByIdAsync(uid);
            if (user != null)
            {
                return await _userManager.ConfirmEmailAsync(user, token);
            }
            return null;

        }

        private async Task SendEmailConfirmationEmail(ApplicationUser user,String token)
        {
           string appDomain = _configuration.GetSection("ApplicatinPaths:AppDomain").Value;
           string link = _configuration.GetSection("ApplicatinPaths:EmailConfirmation").Value;
            UserEmailOptions userEmailOptions = new UserEmailOptions
            {
                ToEmails = new List<string> { user.Email },
                Placeholders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{Guest}}",user.FirstName),
                    new KeyValuePair<string, string>("{{link}}",String.Format(appDomain+link,user.Id,token))
                }

            };
            await _emailService.SendEmailForEmailConfirmation(userEmailOptions);
        }

        private async Task SendForgotPasswordEmail(ApplicationUser user, string token)
        {
            string appDomain = _configuration.GetSection("ApplicatinPaths:AppDomain").Value;
            string link = _configuration.GetSection("ApplicatinPaths:ForgotPassword").Value;
            UserEmailOptions userEmailOptions = new UserEmailOptions
            {
                ToEmails = new List<string> { user.Email },
                Placeholders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{Guest}}",user.FirstName),
                    new KeyValuePair<string, string>("{{link}}",String.Format(appDomain+link,user.Id,token))
                }

            };
            await _emailService.SendEmailForForgotPassword(userEmailOptions);
        }
    }                                                                                    
}
