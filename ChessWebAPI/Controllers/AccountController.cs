using AutoMapper;
using ChessWeb.DAL.Models;
using ChessWeb.Shared;
using ChessWebAPI.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChessWebAPI.Controllers
{

    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("/signup")]
        public async Task<IActionResult> SignUp(SignUpDTO model)
        {
            var user = _unitOfWork.Player.GetByCondition(x => x.Username == model.Username
                || x.EmailAddress == model.EmailAddress).FirstOrDefault();
            
            if (user != null)
            {
                return BadRequest(new { error = "Username or email address already in use!" }); 
            }
            
            Player newUser = _mapper.Map<Player>(model);
            _unitOfWork.Player.Create(newUser);
            _unitOfWork.Complete();

            ClaimsPrincipal principal = AuthenticationHelper.CreateClaims(newUser.PlayerId,
                newUser.Username, newUser.EmailAddress);
            
            AuthenticationProperties authProperties = new AuthenticationProperties()
            {
                ExpiresUtc = DateTimeOffset.Now.AddMinutes(15),
                AllowRefresh = true,
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

            return Ok();
        }

        [HttpPost]
        [Route("/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return NoContent();
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            var user = _unitOfWork.Player.GetByCondition(x => (x.Username == model.Username || x.EmailAddress == model.Username)
                && x.Password == model.Password).FirstOrDefault();

            if (user == null)
            {
                return StatusCode(401, new { error = "Invalid username or password" });
            }

            ClaimsPrincipal principal = AuthenticationHelper.CreateClaims(user.PlayerId,
                user.Username, user.EmailAddress);

            AuthenticationProperties authProperties = new AuthenticationProperties()
            {
                ExpiresUtc = DateTimeOffset.Now.AddMinutes(15),
                AllowRefresh = true,
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

            return Ok();

        }
    }
}
