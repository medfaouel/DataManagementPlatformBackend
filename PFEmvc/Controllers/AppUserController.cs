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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace PFEmvc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
                    return await Task.FromResult(new ResponseModel(Models.Enums.ResponseCode.OK, "User has been Registered",null));
                }
                return await Task.FromResult(new ResponseModel(Models.Enums.ResponseCode.Error, "" , result.Errors.Select(x => x.Description).ToArray()));
            }
            catch (Exception ex) { return await Task.FromResult(new ResponseModel(Models.Enums.ResponseCode.Error, ex.Message, null)); }

        }
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetAllUser")]
        public async Task<object> GetAllUsers()
        {
            try
            {
                var users = _userManager.Users;
                return await Task.FromResult(new ResponseModel(Models.Enums.ResponseCode.OK, "done", users));
            }
            catch (Exception ex) 
            {
                return await Task.FromResult(new ResponseModel(Models.Enums.ResponseCode.Error, ex.Message, null));
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
                        var appUser = await _userManager.FindByEmailAsync(model.Email);
                        var user = new AppUserDTO(appUser.FirstName, appUser.LastName, appUser.Email, appUser.UserName, appUser.DateCreated);
                        user.Token = GenerateToken(appUser);
                        

                        return await Task.FromResult(new ResponseModel(Models.Enums.ResponseCode.OK, "", user));
                    }
                }

                return await Task.FromResult(new ResponseModel(Models.Enums.ResponseCode.Error, "invalid Email or Password", null));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(Models.Enums.ResponseCode.Error, ex.Message, null));
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
            SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature),
            Audience=_jWTConfig.Audience,
            Issuer=_jWTConfig.Issuer,
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);

        }

    }
}
