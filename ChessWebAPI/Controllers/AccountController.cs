using ChessWeb.DAL.Models;
using ChessWeb.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChessWebAPI.Controllers
{

    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Player> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSettings;
        public AccountController(UserManager<Player> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
            _jwtSettings = _configuration.GetSection("JwtSettings");
        }

        [HttpPost]
        [Route("/signup")]
        public async Task<IActionResult> SignUp([FromBody]SignUpDTO registrationModel)
        {
            var user = new Player
            {
                UserName = registrationModel.Username,
                Email = registrationModel.EmailAddress
            };

            var result = await _userManager.CreateAsync(user, registrationModel.Password);

            if (result.Succeeded == false)
            {
                return BadRequest(new SignUpResultDTO
                {   
                    IsSuccessfulyRegistered = false,
                    Errors = result.Errors.Select(e => e.Description) 
                });
            }

            return StatusCode(StatusCodes.Status201Created, new SignUpResultDTO 
            {
                Id = user.Id,
                IsSuccessfulyRegistered = true
            });
        }

        [HttpPost("/signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInDTO signInModel)
        {
            var user = await _userManager.FindByNameAsync(signInModel.Username);
                
            if (user == null || await _userManager.CheckPasswordAsync(user, signInModel.Password) == false)
            {
                return Unauthorized(new SignInResultDTO { ErrorMessage = "Invalid username or password" });
            }
            
            var signingCredentials = GetSigningCredentials();
            var claims = GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            
            return Ok(new SignInResultDTO { IsAuthSuccessful = true, Token = token });
        }


        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings["securityKey"]);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
         
        private static List<Claim> GetClaims(Player user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            return claims;
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtSettings["validIssuer"],
                audience: _jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings["expiryInMinutes"])),
                signingCredentials: signingCredentials);

            return tokenOptions;
        }
    }
}





































/*
[HttpPost]
[Route("/signup")]
public async Task<IActionResult> SignUp(SignUpDTO model)
{
    var user = _unitOfWork.Player.GetByCondition(x => x.Username == model.Username
        || x.EmailAddress == model.EmailAddress).FirstOrDefault();

    if (user != null)
    {
        return StatusCode(409, new { error = "Username or email address already in use!" }); 
    }

    Player newUser = _mapper.Map<Player>(model);
    _unitOfWork.Player.Create(newUser);
    _unitOfWork.Complete();

    ClaimsPrincipal principal = AuthenticationHelper.CreateClaims(newUser.PlayerId,
        newUser.Username, newUser.EmailAddress);

    AuthenticationProperties authProperties = new AuthenticationProperties()
    {
        ExpiresUtc = DateTimeOffset.Now.AddMinutes(10),
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
        ExpiresUtc = DateTimeOffset.Now.AddMinutes(10),
        AllowRefresh = true,
        IsPersistent = true,
    };

    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

    return Ok();

}*/

