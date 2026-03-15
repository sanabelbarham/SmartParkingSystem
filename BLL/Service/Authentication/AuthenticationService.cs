using DAL.DTO.Request;
using DAL.DTO.Request.Registor;
using DAL.DTO.Responce;
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
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticationService(UserManager<ApplicationUser> userManager,IConfiguration configuration,
            IEmailSender emailSender, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailSender = emailSender;
            _signInManager = signInManager;
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


            if(await _userManager.IsLockedOutAsync(user))
                {
                    return new LoginResponce
                    {
                        Success = false,
                        Message = "the account is louked out "
                    };
                }



                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);

            
            if (result.IsLockedOut)
            {
                return new LoginResponce
                {
                    Message = "the account is locked out  dur to multipul trys",
                    Success = false
                };
            }


            else if (result.IsNotAllowed)
                {
                    return new LoginResponce
                    {
                        Message = "plz confirm your email",
                        Success = false
                    };
                }

            else if(!result.Succeeded)
                {

                    return new LoginResponce
                    {
                        Message = "invalid passwoed plz try again",
                        Success = false
                    };
                }


                return new LoginResponce
                    {
                        Message = "Login Successfuly",
                        Success = true,
                        AccessToken = await GenerateAccessToken(user)

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
            var roles = await _userManager.GetRolesAsync(user);
            var userClaim = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, string.Join(',',roles))
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
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                token = Uri.EscapeDataString(token);
                var ConfirmEmail = $"https://localhost:7250/api/auth/Account/ConfirmEmail?token={token}&userId={user.Id}";
                await _userManager.AddToRoleAsync(user, "User");
                await _emailSender.SendEmailAsync(request.Email, "Confirm email", $"<h1>hi</h1> " +
                 $"<a href={ConfirmEmail}></a>" +
                    "");


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


        public async Task<bool>ConfirmEmailAsync(string token,string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user is null)
            {
                return false;
            }
           var result= await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
              { return false; }
            return true;
        
        }

        public async Task<ForgetPasswordResponce> ReqestPasswordResetAsync(ForgetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                return new ForgetPasswordResponce
                {
                    Success = false,
                    Message = "no user with this email "
                };
            }
            var random = new Random();
            var code = random.Next(200, 10000).ToString();
            user.PaswordResetCode = code;
            user.PaswordResetCodeExpiary = DateTime.UtcNow.AddMinutes(15);
            await _userManager.UpdateAsync(user);
            await _emailSender.SendEmailAsync(user.Email, "reset password", $"<h1> the code is {code}</h1>");
            return new ForgetPasswordResponce
            {
                Message = "code sent succefully",
                Success = true
            };


        }

        public async Task<ResetPasswordResponce> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                return new ResetPasswordResponce
                {
                    Success = false,
                    Message = "no user with this email "
                };
            }

            if (user.PaswordResetCode != request.Code)
            {
                return new ResetPasswordResponce
                {
                    Success = false,
                    Message = "the code is not the same"
                };
            }
            if (user.PaswordResetCodeExpiary < DateTime.UtcNow)
            {
                return new ResetPasswordResponce
                {
                    Success = false,
                    Message = "the code is expired"
                };
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, request.Password);
            if (!result.Succeeded)
            {
                return new ResetPasswordResponce
                {
                    Success = false,
                    Message = "reset password proccess failed "
                };
            }
            await _emailSender.SendEmailAsync(request.Email, "<h1>reset password</h1>", "<p> reset password is done</p>");

            return new ResetPasswordResponce
            {
                Success = true,
                Message = "reset password proccess is done "
            };

        }

    }
}
