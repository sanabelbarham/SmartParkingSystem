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

        [HttpPost("Registor")]
        public async Task<IActionResult> Registor(RegistorRequests registerRequest)
        {
            var responce = await _authenticationService.RegistorAsync(registerRequest);
            return Ok(responce);

        }



        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token,string userId )
        {
            var responce = await _authenticationService.ConfirmEmailAsync(token,userId);
            return Ok(responce);

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var responce = await _authenticationService.LoginAsync(request);
            return Ok(responce);
        }

        [HttpPost("ResetCode")]
        public async Task<IActionResult> ResetCode(ForgetPasswordRequest request)
        {
            var responce = await _authenticationService.ReqestPasswordResetAsync(request);
            return Ok(responce);
        }

        [HttpPatch("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var responce = await _authenticationService.ResetPasswordAsync(request);
            if(responce is not null)
            return Ok(responce);
            return BadRequest(responce);
        }

}
    }
