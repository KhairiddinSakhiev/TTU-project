using Domain.AccountDto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.AccountServices
{
    public  class AccountService
    {

        private readonly UserManager<IdentityUser> _userManager;

        public AccountService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }


        public async Task<IdentityUser> Login(LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null) return null;

            var validatePassword = new PasswordValidator<IdentityUser>();
            var result = await validatePassword.ValidateAsync(_userManager, user, model.Password);
            if (result.Succeeded == false) return null;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.UserName)
            };

            await _userManager.AddClaimsAsync(user, claims);

            return user;
        }

        public async Task<bool> Register(Register register)
        {
            var user = new IdentityUser
            {
                Email = register.Email,
                UserName = register.UserName
            };
            var result = await _userManager.CreateAsync(user);
            return result.Succeeded;
        }
    }
}
