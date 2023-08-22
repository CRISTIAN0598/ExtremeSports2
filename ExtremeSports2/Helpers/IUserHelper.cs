﻿using ExtremeSports2.Data.Entities;
using ExtremeSports2.Models;
using Microsoft.AspNetCore.Identity;

namespace ExtremeSports2.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserAsync(string email);

        Task<User> GetUserAsync(Guid userId);
        Task<IdentityResult> AddUserAsync(User user, string password);

        //Task<User> AddUserAsync(AddUserViewModel model);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(User user, string roleName);

        Task<bool> IsUserInRoleAsync(User user, string roleName);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();
    }
}
