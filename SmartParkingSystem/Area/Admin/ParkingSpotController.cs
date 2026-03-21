using BLL.Service;
using DAL.Data;
using BLL.Service;
using DAL.Data;
using DAL.DTO.Request;
using DAL.DTO.Responce;
using DAL.Migrations;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SmartParkingSystem.Resources;
using System.Threading.Tasks;


namespace SmartParkingSystem.Area.Admin
{
    [Route("api/Admin/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ParkingSpotController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IParkingSpotService _parkingSpotService;
        public ParkingSpotController(ApplicationDbContext context, IStringLocalizer<SharedResources> stringLocalizer,
           IParkingSpotService parkingSpotService)
        {
            _context = context;
            _stringLocalizer = stringLocalizer;
            _parkingSpotService = parkingSpotService;
        }

        [HttpPost("createParking")]
        public async Task<IActionResult> CreateParking(ParkingSpotRequest request)
        {

            var responce = await _parkingSpotService.CreatParkingSpot(request);
            return Ok(new { message = _stringLocalizer["Success"].Value, responce });
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteParking([FromRoute]int id)
        {
            var result = await _parkingSpotService.DeleteSpotAsync(id);
            if (result is null)
            {
                if (result.Message.Contains("no parking spot with this id"))
                {
                    return NotFound();
                }
                return BadRequest();
            }
            return Ok(new { message = _stringLocalizer["Success"].Value });
        }

        [HttpPatch("{id}")]
        public async Task <IActionResult> UpdateSpot([FromRoute] int id,ParkingSpotRequest request)
        {
            var responce = await _parkingSpotService.UpdateSpotAsync(id,request);
            return Ok(new { message = _stringLocalizer["Success"].Value, responce });
        }

        [HttpGet("GetAvailableSpots")]
        public IActionResult GetSpots()
        {

            var responce = _parkingSpotService.GetAvaliableSpotsAsync();
            return Ok(new { message = _stringLocalizer["Success"].Value, responce });
        }


    }

}
