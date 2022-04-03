using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PFEmvc;
using PFEmvc.dto;
using PFEmvc.Models;
using WebApplicationPFE.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace PFEmvc.Controllers
{
    public class AppUserController : Controller

    {  
        private readonly ILogger<AppUserController> _logger;
        private readonly JWTConfig _jWTConfig;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _SignInManager;
        public AppUserController(DbContextApp context, IOptions<JWTConfig> jWTConfig, ILogger<AppUserController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> SignInManager)
        {
            
            _logger = logger;
            _userManager = userManager;
            _SignInManager = SignInManager;
            _jWTConfig = jWTConfig.Value;

        }
        [HttpPost("RegisterUser")]
        public async Task<object> RegisterUser([FromBody] dto.identityUserModel model)
        {
            try
            {


                var user = new AppUser() { FirstName = model.FirstName, Email = model.Email, LastName = model.LastName,UserName=model.Email, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return await Task.FromResult("User has been Registered");
                }
                return await Task.FromResult(string.Join(",", result.Errors.Select(x => x.Description).ToArray() ));
            }
            catch (Exception ex) { return await Task.FromResult(ex.Message); }

        }
        [HttpGet("GetAllUser")]
        public async Task<object> GetAllUsers()
        {
            try
            {
                var users = _userManager.Users;
                return await Task.FromResult(users);
            }
            catch (Exception ex) 
            {
                return await Task.FromResult(ex.Message);
            }

        }
        [HttpPost("Login")]
        public async Task<object> Login([FromBody] LoginBindingModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var result = await _SignInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        return await Task.FromResult("Login Successfully");
                    }
                }
                return await Task.FromResult("invalid Email or Password");
            }
            catch (Exception ex)
            {
                return await Task.FromResult(ex.Message);
            }
        }
        private string GenerateToken(AppUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jWTConfig.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]{
                new System.Security.Claims.Claim(JwtRegisteredClaimNames.NameId,user.Id),
                new System.Security.Claims.Claim(JwtRegisteredClaimNames.Email,user.Email),
                new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),

            }),
            Expires=DateTime.UtcNow.AddHours(12),
            SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };
    }

    }
}
