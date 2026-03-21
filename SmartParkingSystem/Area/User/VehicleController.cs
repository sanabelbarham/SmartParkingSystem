using BLL.Service.Vehicle;
using DAL.DTO.Request;
using DAL.DTO.Request.Vehicle;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace SmartParkingSystem.Area.User
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpPost("CreateVehicle")]
        public async Task<IActionResult> CreateVehicle(VehicleRequest vehicleRequest)
        {

            var responce = _vehicleService.CreateVehicleAsync(vehicleRequest);
            return Ok(new { responce });
        }



        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteParking([FromRoute] int id)
        {
            var result = await _vehicleService.DeleteVehicleAsync(id);
            if (result is null)
            {
                if (result.Message.Contains("no Vehicle with this id"))
                {
                    return NotFound();
                }
                return BadRequest();
            }
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateSpot([FromRoute] int id, VehicleRequest request)
        {
            var responce = await _vehicleService.UpdateVehicleAsync(id, request);
            return Ok(new {responce });
        }

        //[HttpGet("GetAvailableSpots")]
        //public IActionResult GetSpots()
        //{

        //    var responce = _vehicleService.GetVehicleAsync();
        //    return Ok(new {  responce });
        //}


    }
}
