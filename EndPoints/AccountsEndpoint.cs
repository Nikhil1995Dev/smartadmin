using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SmartAdmin.WebUI.Data;
using SmartAdmin.WebUI.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;


namespace SmartAdmin.WebUI.EndPoints
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsEndpoint : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly SmartSettings _settings;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountsEndpoint(ApplicationDbContext context
            , SmartSettings settings
            , UserManager<ApplicationUser> userManager
            , SignInManager<ApplicationUser> signInManager
            )
        {
            _context = context;
            _settings = settings;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpPost]
        // [ProducesResponseType(StatusCodes.Status201Created)]
        [Route("authenticate")]
        [AllowAnonymous]
        public async Task<Response> Login(LoginModel model)
        {
            //try
            //{
            //    var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, true, lockoutOnFailure: true);
            //    if (result.Succeeded)
            //    {
            //        return CreatedAtAction("Get", new { id = "" }, model);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
            //return BadRequest();


            Response response = new Response();
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                response.Status = 201;
                response.Message = "We do not have any account with this username.";
                response.Data = null;
                return response;
            }
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, lockoutOnFailure: true);
            if (result.IsLockedOut)
            {
                response.Status = 401;
                response.Message = "User account locked out.";
                response.Data = null;
                return response;
            }
            else if (result.Succeeded)
            {
                if (user != null)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes("uhdfiusdhf9werwerfwe9fwodniwne9fdw8efndwjekdnw9efuwnekfdjwe9f");
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new GenericIdentity(model.Username, "TokenAuth"), new Claim[]
                        {
                        new Claim(ClaimTypes.Name, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, "AppUser"),
                        new Claim(ClaimTypes.Email, user.Email.ToString()),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                        }),
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);

                    response.Status = 200;
                    response.Message = "User logged in.";
                    response.Data = new
                    {
                        Id = user.Id,
                        Username = user.UserName,
                        Name = user.Name,
                        Role = model.Role,
                        Token = tokenString
                    };
                    return response;
                }
                else
                {
                    response.Status = 402;
                    response.Message = "Unauthorized.";
                    response.Data = null;
                    return response;
                }
            }
            else
            {
                response.Status = 402;
                response.Message = "Unauthorized.";
                response.Data = null;
                return response;
            }
        }

        [HttpGet]
        [Route("user-profile")]
        public async Task<Response> UserProfile()
        {
            Response response = new Response();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                response.Status = 201;
                response.Message = "We do not have any account with this username.";
                response.Data = null;
            }
            else
            {
                response.Status = 200;
                response.Message = "User logged in.";
                response.Data = new
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Name = user.Name,
                    Role = "AppUser"
                };
            }
            return response;
        }
        public class LoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
        }
        public class Response
        {
            public int Status { get; set; }
            public string Message { get; set; }
            public dynamic Data { get; set; }
        }
    }
}
