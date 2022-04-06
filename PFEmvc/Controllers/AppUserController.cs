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
using PFEmvc.Models.Enums;
using System.Security.Claims;

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
        private readonly RoleManager<IdentityRole> _roleManager;
        public AppUserController(DbContextApp context, IOptions<JWTConfig> jWTConfig, RoleManager<IdentityRole> roleManager, ILogger<AppUserController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> SignInManager)
        {
            _roleManager = roleManager;
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

                if (!await _roleManager.RoleExistsAsync(model.Role))
                {

                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Role does not exist", null));
                }
                var user = new AppUser() { FirstName = model.FirstName, Email = model.Email, LastName = model.LastName,UserName=model.Email, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var tempUser = await _userManager.FindByEmailAsync(model.Email);
                    await _userManager.AddToRoleAsync(tempUser, model.Role);
                    return await Task.FromResult(new ResponseModel(Models.Enums.ResponseCode.OK, "User has been Registered",null));
                }
                return await Task.FromResult(new ResponseModel(Models.Enums.ResponseCode.Error, "" , result.Errors.Select(x => x.Description).ToArray()));
            }
            catch (Exception ex) { return await Task.FromResult(new ResponseModel(Models.Enums.ResponseCode.Error, ex.Message, null)); }

        }
        // role admin
        [Authorize(Roles = "admin")]
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetAllUser")]
        public async Task<object> GetAllUsers()
        {
            try
            {
                List<AppUserDTO> allUserDTO = new List<AppUserDTO>();
                var users = _userManager.Users.ToList();
                foreach (var user in users)
                {
                    var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

                    allUserDTO.Add(new AppUserDTO(user.FirstName,user.LastName, user.Email, user.UserName, user.DateCreated, role));
                }
                return await Task.FromResult(new ResponseModel(Models.Enums.ResponseCode.OK, "done", allUserDTO));
            }
            catch (Exception ex) 
            {
                return await Task.FromResult(new ResponseModel(Models.Enums.ResponseCode.Error, ex.Message, null));
            }

        }
        //role user
        [Authorize(Roles ="user")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetUsers")]
        public async Task<object> GetUsers()
        {
            try
            {
                List<AppUserDTO> allUserDTO = new List<AppUserDTO>();
                var users = _userManager.Users.ToList();
                foreach (var user in users)
                {
                    var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                    if(role == "user")
                    {
                        allUserDTO.Add(new AppUserDTO(user.FirstName, user.LastName, user.Email, user.UserName, user.DateCreated, role));
                    }

                   
                }
                return await Task.FromResult(new ResponseModel(Models.Enums.ResponseCode.OK, "done", allUserDTO));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(Models.Enums.ResponseCode.Error, ex.Message, null));
            }

        }
        [HttpGet("GetRoles")]
        public async Task<object> GetRoles()
        {
            try
            {

                var roles = _roleManager.Roles.Select(x => x.Name).ToList();

                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", roles));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
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
                        var role = (await _userManager.GetRolesAsync(appUser)).FirstOrDefault();
                        var user = new AppUserDTO(appUser.FirstName, appUser.LastName, appUser.Email, appUser.UserName, appUser.DateCreated,role);
                        user.Token = GenerateToken(appUser,role);
                        

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
        private string GenerateToken(AppUser user,string role)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jWTConfig.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]{
                new System.Security.Claims.Claim(JwtRegisteredClaimNames.NameId,user.Id),
                new System.Security.Claims.Claim(JwtRegisteredClaimNames.Email,user.Email),
                new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new System.Security.Claims.Claim(ClaimTypes.Role,role),


            }),
            Expires=DateTime.UtcNow.AddHours(12),
            SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature),
            Audience=_jWTConfig.Audience,
            Issuer=_jWTConfig.Issuer,
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);

        }
        [Authorize(Roles = "admin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("AddRole")]
        public async Task<object> AddRole([FromBody] AddRoleBindingModel model)
        {
            try
            {
                if (model == null || model.Role == "")
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "parameters are missing", null));

                }
                if (await _roleManager.RoleExistsAsync(model.Role))
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Role already exist", null));

                }
                var role = new IdentityRole();
                role.Name = model.Role;
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {

                    return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Role added successfully", null));
                }
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, "something went wrong please try again later", null));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
            }
        }

    }
}
