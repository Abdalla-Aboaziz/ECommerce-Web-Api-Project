using ECommerce.ServiceAbstraction;
using ECommerce.Shared.IdentityDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presentation.Controllers
{
    public class AuthenticationController : ApiBaseController
    {
        private readonly IAuthenticationSerivce _authenticationSerivce;

        public AuthenticationController(IAuthenticationSerivce authenticationSerivce)
        {
            _authenticationSerivce = authenticationSerivce;
        }


        //post
        //api/authentication/login
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var result = await _authenticationSerivce.LoginAsync(loginDto);

            return HandelResult(result);
        }
        [HttpPost("Register")]
        // api/authentication/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var result = await _authenticationSerivce.RegisterAsync(registerDto);

            return HandelResult(result);
        }
        [HttpGet("EmailExist")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var result = await _authenticationSerivce.CheckEmailAsync(email);
            return Ok(result);
        }
        [HttpGet("CurrentUser")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await _authenticationSerivce.GetUserByEmailAsync(email!);
            return HandelResult(result);

        }
    }
}
