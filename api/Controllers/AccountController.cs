using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.Dtos.User;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController:ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly ITokenService tokenService;
        private readonly SignInManager<AppUser> signInManager;
        
        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,ITokenService tokenService)
        {
            this.signInManager = signInManager;
            this.tokenService = tokenService;
            this.userManager = userManager;
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var user = await userManager.Users.FirstOrDefaultAsync(user=>user.UserName==loginDto.Username.ToLower());

            if(user == null) return Unauthorized("Invalid username!");

            var result = await signInManager.CheckPasswordSignInAsync(user,loginDto.Password,false);

            if(!result.Succeeded) return Unauthorized("Username or password notfound or incorrect");
            return Ok(
                new NewUserDto{
                    UserName=user.UserName,
                    Email=user.Email,
                    Token=tokenService.CreateToken(user)
                }
            );


        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var appUser = new AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email

                };

                var createdUser = await userManager.CreateAsync(appUser, registerDto.Password);
                if (createdUser.Succeeded)
                {
                    var roleResult = await userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto {
                                UserName = appUser.UserName,
                                Email=appUser.Email,
                                Token=tokenService.CreateToken(appUser)
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }

                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }

            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }



    }
}