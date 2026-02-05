using ECommerce.Domain.Entities.IdentityModule;
using ECommerce.ServiceAbstraction;
using ECommerce.Shared.CommonResult;
using ECommerce.Shared.IdentityDtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service
{
    public class AuthenticationSerivce : IAuthenticationSerivce
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticationSerivce(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Result<UserDto>> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                return Error.InvalidCredintails("User InvalidCred");

            var IsPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!IsPasswordValid)
                return Error.InvalidCredintails("Password InvalidCred");

            return new UserDto(user.Email,user.DisplayName,"Token");
         
        }

        public async Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto)
        {
           var User = new ApplicationUser
            {
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                UserName = registerDto.UserName,
                PhoneNumber = registerDto.PhoneNumber
            };
            var result = await _userManager.CreateAsync(User, registerDto.Password);
            if(result.Succeeded)
                return new UserDto(User.Email, User.DisplayName, "Token");

            return result.Errors.Select(e => Error.Validation(e.Code,e.Description)).ToList();

        }
    }
}
