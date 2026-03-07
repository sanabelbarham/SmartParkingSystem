using DAL.DTO.Request.Registor;
using DAL.DTO.Responce.Registor;
using DAL.Identity;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticationService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<RegistorResponce> RegistorAsync(RegistorRequests request)
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
                Errors =null
            }; 


        }
    }
}
