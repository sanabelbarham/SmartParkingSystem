using BLL.Service.Authentication;
using Microsoft.AspNetCore.Http;

using DAL.DTO.Request.Registor;
using DAL.DTO.Responce.Registor;

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SmartParkingSystem.Area.Identity
{
    [Route("api/auth/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("")]
        public async Task<IActionResult> Registor(RegistorRequests registerRequest)
        {
            var responce = await _authenticationService.RegistorAsync(registerRequest);
            return Ok(responce);

        }
    }
}
