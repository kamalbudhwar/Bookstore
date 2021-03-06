﻿using BookStore.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel);
        Task<SignInResult> PasswordSignInAsync(SignInModel signInModel);
        Task SignOutAsync();
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel model);
        Task<IdentityResult> ConfirmEmailAsync(string uid, string token);
        Task GenerateTokenToConfirmEmailAsync(ApplicationUser user);
        Task<ApplicationUser> GetUserByEmailId(string email);
        Task GenerateTokenForForgetPasswordAsync(ApplicationUser user);
        Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model);
    }
}