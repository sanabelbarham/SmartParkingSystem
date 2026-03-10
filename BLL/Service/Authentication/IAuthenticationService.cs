using DAL.DTO.Request.Registor;
using DAL.DTO.Responce.Registor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service.Authentication
{
  public  interface IAuthenticationService
    {
        Task<RegistorResponce> RegistorAsync(RegistorRequests request);
        Task<LoginResponce> LoginAsync(LoginRequest request);
        Task<bool> ConfirmEmailAsync(string token, string userId);

    }
}
