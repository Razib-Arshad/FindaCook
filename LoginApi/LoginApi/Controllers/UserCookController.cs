﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

//using LoginApi.Data;//

using LoginApi.Models;
using LoginApi.ViewModels;
using Microsoft.AspNetCore.Authorization;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Net;

namespace LoginApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCookController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public UserCookController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _configuration = configuration;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Add(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = new ApplicationUser
                {
                    UserName = model.Username,
                    Email = model.Email,
                    UserPassword = model.Password
                };

                var userInfo = new User
                {
                    UserName = model.Username,
                    Email = model.Email,
                    UserPassword = model.Password
                };

                IdentityResult? result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _context.Users.Add(userInfo);
                    await _context.SaveChangesAsync();
                    return Ok("User created successfully");
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }

            return BadRequest("Invalid Request");
        }

        [HttpPost("RegisterCook")]
        public async Task<ActionResult> RegisterCook(CookInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Additional logic to create CookInfo
                var cookInfo = new CookInfo
                {
                    Email = model.Email,
                    UserPassword=model.UserPassword,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    ContactNumber = model.ContactNumber,
                    WhatsappNumber = model.WhatsappNumber,
                    CurrentAddress = model.CurrentAddress,
                    PermanentAddress = model.PermanentAddress,
                    EligibleToWork = model.EligibleToWork,
                    HasCulinaryDegree = model.HasCulinaryDegree,
                    Degree = model.Degree,
                    Certificates = model.Certificates,
                    CulinarySchoolName = model.CulinarySchoolName,
                    ExperienceYears = model.ExperienceYears,
                    SkillsAndSpecialties = model.SkillsAndSpecialties,
                    SignatureDishes = model.SignatureDishes,
                    ServicesProvided = model.ServicesProvided
                };

                _context.CookInfos.Add(cookInfo);
                await _context.SaveChangesAsync();
                return Ok("User registered as a cook successfully");
            }
            
            
            return BadRequest("Invalid Request");
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    // Add other claims as needed
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            Console.WriteLine(model);
            Console.WriteLine($"Received login request for email: {model.Email}");

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                Console.WriteLine(user);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        var token = GenerateJwtToken(user);
                        var responseObject = new
                        {
                            User = new
                            {
                                Id = user.Id,
                                UserName = user.UserName,
                                UserEmail = user.Email

                            },
                            Token = token,
                            Message = "Login successful"
                        };

                        return Ok(responseObject);
                    }
                    else
                    {
                        var responseObject = new
                        {
                            User = (object)null,
                            Token = (string)null,
                            Message = "Invalid login attempt"
                        };

                        return BadRequest(responseObject);
                    }
                }
                else
                {
                    var responseObject = new
                    {
                        User = (object)null,
                        Token = (string)null,
                        Message = "Invalid login attempt. Object not found"
                    };

                    return BadRequest(responseObject);
                }
            }
            return BadRequest("Invalid Request");
        }

        [HttpGet("categories/get/servicesprovided")]
        public async Task<ActionResult> GetCooksByServicesProvided()
        {
            try
            {
                var cooks = await _context.CookInfos
                    .Where(c => !string.IsNullOrEmpty(c.ServicesProvided))
                    .Select(c => new
                    {
                        c.FirstName,
                        c.LastName,
                        c.Email,
                        c.SkillsAndSpecialties,
                        c.SignatureDishes,
                        c.ServicesProvided,
                        c.ExperienceYears,
                        c.CulinarySchoolName,
                        c.HasCulinaryDegree,
                        c.Degree,
                        c.Certificates,
                        c.EligibleToWork
                    })
                    .ToListAsync();

                var response = new
                {
                    StatusCode = 200,
                    Message = "Cooks with services provided retrieved successfully",
                    Data = cooks
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                var errorResponse = new
                {
                    StatusCode = 500,
                    Message = "An error occurred while retrieving cooks",
                    ErrorDetails = ex.Message
                };

                return StatusCode(500, errorResponse);
            }
        }

        [HttpGet("categories/get/signaturedishes")]
        public async Task<ActionResult> GetCooksBySignatureDishes()
        {
            try
            {
                var cooks = await _context.CookInfos
               .Where(c => !string.IsNullOrEmpty(c.SignatureDishes))
               .Select(c => new
               {
                   c.FirstName,
                   c.LastName,
                   c.Email,
                   c.SkillsAndSpecialties,
                   c.SignatureDishes,
                   c.ServicesProvided,
                   c.ExperienceYears,
                   c.CulinarySchoolName,
                   c.HasCulinaryDegree,
                   c.Degree,
                   c.Certificates,
                   c.EligibleToWork
               })
               .ToListAsync();

                var response = new
                {
                    StatusCode = 200,
                    Message = "Cooks with signature dishes provided retrieved successfully",
                    Data = cooks
                };

                return Ok(response);
            }
            catch(Exception ex)
            {
                // Log the exception or handle it as needed
                var errorResponse = new
                {
                    StatusCode = 500,
                    Message = "An error occurred while retrieving cooks",
                    ErrorDetails = ex.Message
                };

                return StatusCode(500, errorResponse);
            }
           
        }

        [HttpGet("search/bycategory")]
        public async Task<ActionResult> SearchByCategory(string category)
        {
            try
            {
                var cooks = await _context.CookInfos
                .Where(c => c.SkillsAndSpecialties.Contains(category))
                 .Select(c => new
                 {
                     c.Id,
                     c.FirstName,
                     c.LastName,
                     c.Email,
                     c.SkillsAndSpecialties,
                     c.SignatureDishes,
                     c.ServicesProvided,
                     c.ExperienceYears,
                     c.CulinarySchoolName,
                     c.HasCulinaryDegree,
                     c.Degree,
                     c.Certificates,
                     c.EligibleToWork
                 })
                .ToListAsync();

                var response = new
                {
                   
                    Data = cooks
                };


                return Ok(response);
            }
            catch(Exception ex)
            {
                // Log the exception or handle it as needed
                var errorResponse = new
                {
                    StatusCode = 500,
                    Message = "An error occurred while retrieving cooks",
                    ErrorDetails = ex.Message
                };

                return StatusCode(500, errorResponse);
            }
            
        }

        [HttpGet("search/bycook")]
        public async Task<ActionResult> SearchByCook(string cookName)
        {
            try
            {
                var cooks = await _context.CookInfos
                .Where(c => c.FirstName.Contains(cookName) || c.LastName.Contains(cookName))
                .Select(c => new
                {
                    c.FirstName,
                    c.LastName,
                    c.Email,
                    c.SkillsAndSpecialties,
                    c.SignatureDishes,
                    c.ServicesProvided,
                    c.ExperienceYears,
                    c.CulinarySchoolName,
                    c.HasCulinaryDegree,
                    c.Degree,
                    c.Certificates,
                    c.EligibleToWork
                })
                .ToListAsync();

                var response = new
                {
                    StatusCode = 200,
                    Message = "Cooks retrieved successfully",
                    Data = cooks
                };


                return Ok(response);
            }
            catch(Exception ex)
            {
                // Log the exception or handle it as needed
                var errorResponse = new
                {
                    StatusCode = 500,
                    Message = "An error occurred while retrieving cooks",
                    ErrorDetails = ex.Message
                };

                return StatusCode(500, errorResponse);
            }
            
        }

        [HttpPut("profile/update_cook")]
        public async Task<ActionResult> UpdateProfile(CookInfoViewModel model)
        {
            var cookInfo = await _context.CookInfos.SingleOrDefaultAsync(c => c.Id == model.Id);
            if (cookInfo == null)
            {
                var errorResponse = new
                {
                    StatusCode = 404,
                    Message = "Not Found",
                };

                return NotFound(errorResponse);
            }
            else
            {
                try
                {
                    cookInfo.FirstName = model.FirstName;
                    cookInfo.LastName = model.LastName;
                    cookInfo.ContactNumber = model.ContactNumber;
                    cookInfo.WhatsappNumber = model.WhatsappNumber;
                    cookInfo.CurrentAddress = model.CurrentAddress;
                    cookInfo.PermanentAddress = model.PermanentAddress;
                    cookInfo.EligibleToWork = model.EligibleToWork;
                    cookInfo.HasCulinaryDegree = model.HasCulinaryDegree;
                    cookInfo.Degree = model.Degree;
                    cookInfo.Certificates = model.Certificates;
                    cookInfo.CulinarySchoolName = model.CulinarySchoolName;
                    cookInfo.ExperienceYears = model.ExperienceYears;
                    cookInfo.SkillsAndSpecialties = model.SkillsAndSpecialties;
                    cookInfo.SignatureDishes = model.SignatureDishes;
                    cookInfo.ServicesProvided = model.ServicesProvided;

                    await _context.SaveChangesAsync();
                    var response = new
                    {
                        StatusCode = 200,
                        Message = "Profile updated successfully",
                        Data = cookInfo
                    };


                    return Ok(response);
                }
                catch(Exception ex)
                {
                    // Log the exception or handle it as needed
                    var errorResponse = new
                    {
                        StatusCode = 500,
                        Message = "An error occurred while updating cook's profile",
                        ErrorDetails = ex.Message
                    };

                    return StatusCode(500, errorResponse);
                }
            }
        }

        [HttpPut("password/change")]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                var errorResponse = new
                {
                    StatusCode = 404,
                    Message = "Not Found",
                };

                return NotFound(errorResponse);
            }

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var response = new
                {
                    StatusCode = 200,
                    Message = "Password changed successfully",
                    Data = result
                };
                return Ok(response);
            }
            else
            {
                var response = new
                {
                    Message = "Password not changed",
                    Data = result.Errors
                };
                return BadRequest(response);
            }
        }

    }
}
