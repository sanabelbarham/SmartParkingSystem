using BLL.Service.Reservations;
using DAL.DTO.Request.Reservations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmartParkingSystem.Area.User
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        
        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult>CreateReservation(ReservationRequest reservationRequest)
        {
            var responce = await _reservationService.CreateReservationAsync(reservationRequest);
            return StatusCode(500, responce);

        }


        [HttpGet("get")]
        public async Task<IActionResult>  GetReservation( )
        {
            var responce = await _reservationService.GetReservationAsync();
            return Ok(new { responce });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation( [FromRoute]int  id)
        {
            var responce = await _reservationService.DeleteReservationAsync(id);
            return Ok(new { responce });

        }
    }
}
