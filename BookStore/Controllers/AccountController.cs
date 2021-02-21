using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [Route("signup")]
        public IActionResult SignUp()
        {
            return View();
        }
        [Route("signup")]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpUserModel signUpUserModel)
        {
            if (ModelState.IsValid)
            {

                var result =await _accountRepository.CreateUserAsync(signUpUserModel);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    };
                }
                ModelState.Clear();
                //RedirectToAction("confirm-email", new { email = signUpUserModel.Email });
            }
            return View(signUpUserModel);
        }

        [Route("login")]
        public IActionResult LogIn()
        {
            return View();
        }
        
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> LogIn(SignInModel signInModel,String returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result=await _accountRepository.PasswordSignInAsync(signInModel);
                if (result.Succeeded)
                {
                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                   return RedirectToAction("Index","Home");
                }
                if (result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Not allowed");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Crendential");
                }        
            }

            return View(signInModel);
        }

        [Route("logout")]
        public async Task<IActionResult> LogOut()
        {
            await _accountRepository.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Route("change-password")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Route("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var result=await _accountRepository.ChangePasswordAsync(model);
                if (result.Succeeded)
                {
                    ModelState.Clear();
                    ViewBag.Success = true;
                    return View();
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

 
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(String uid,String token,String email)
        {
            EmailConfirmModel emailConfirmModel = new EmailConfirmModel
            {
                Email = email
            };
            if (!String.IsNullOrEmpty(uid) && !String.IsNullOrEmpty(token))
            {
                token = token.Replace(' ','+');
               var result= await _accountRepository.ConfirmEmailAsync(uid, token);
                if (result.Succeeded)
                {
                    emailConfirmModel.EmailVerified = true;
                }
            }
            return View(emailConfirmModel);
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(EmailConfirmModel model)
        {
            var user= await _accountRepository.GetUserByEmailId(model.Email);
            if (user != null)
            {
                if (user.EmailConfirmed)
                {
                    model.EmailVerified = true;
                    return View(model);
                }
                await _accountRepository.GenerateTokenToConfirmEmailAsync(user);
                model.EmailSent = true;
                ModelState.Clear();
            }
            else
            {
                ModelState.AddModelError("","User is not found with this specific ID");
                
            }
            return View(model);

        }

        [AllowAnonymous, HttpGet]
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }

        [AllowAnonymous, HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _accountRepository.GetUserByEmailId(model.Email);
                if (user != null)
                {
                    await _accountRepository.GenerateTokenForForgetPasswordAsync(user);
                    ModelState.Clear();
                    model.EmailSent = true;
                }
            }
            return View(model);
        }
        [AllowAnonymous, HttpGet("reset-password")]
        public async Task<IActionResult> ResetPassword(string uid, string token)
        {
            ResetPasswordModel model = new ResetPasswordModel
            {
                UserId = uid,
                Token = token
            };
            return View(model);
        }
        [AllowAnonymous, HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                model.Token = model.Token.Replace(' ', '+');
                var result = await _accountRepository.ResetPasswordAsync(model);
                if (result.Succeeded)
                {
                    ModelState.Clear();
                    model.isSuccess = true;
                    return View(model);
                }
                else
                {
                    foreach(var err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }
            }
            return View(model);
        }

    }
}
                      