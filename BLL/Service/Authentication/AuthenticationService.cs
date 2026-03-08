using DAL.DTO.Request.Registor;
using DAL.DTO.Responce.Registor;
using DAL.Identity;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthenticationService(UserManager<ApplicationUser> userManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<LoginResponce> LoginAsync(LoginRequest request)
        {
         try
        {    var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                return new LoginResponce
                {
                    Message = "invalid email",
                    Success = false
                };

            }
            var passwordCheck = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordCheck)
            {
                return new LoginResponce
                {
                    Message = "invalid Password",
                    Success = false
                };
            }

                return new LoginResponce
                {
                    Message = "Login Successfuly",
                    Success = true,
                    AccessToken=await GenerateAccessToken(user)

                };
            }
            catch(Exception e)
            {
                return new LoginResponce
                {
                    Message = "An UnExpected error",
                    Success = false,
                    Errors = new List<string> { e.Message }
                };
            }
        }

        private async Task< string> GenerateAccessToken(ApplicationUser user)
        {
            //claim== payload== the body of the token ==> what you want to put inisde it
            var userClaim = new List<Claim>() {
            new Claim("id",user.Id),
            new Claim("name",user.UserName),
            new Claim("email",user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: userClaim,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        public async Task<RegistorResponce> RegistorAsync(RegistorRequests request)
        {


            try
            {


                var user = request.Adapt<ApplicationUser>();
                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    return new RegistorResponce
                    {

                        Message = "Registor faild",
                        Errors = result.Errors.Select(e => e.Description).ToList()
                    };

                }
                await _userManager.AddToRoleAsync(user, "User");
                return new RegistorResponce
                {
                    Message = "Registor succeeded",
                    Errors = null
                };


            }
            catch (Exception e)
            {
                return new RegistorResponce
                {
                    Message = "UnExpected error ",
                    Errors = new List<string> { e.Message }

                };
            }

        }
    }
}
