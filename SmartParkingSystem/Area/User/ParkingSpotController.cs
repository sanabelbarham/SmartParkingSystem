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

namespace SmartParkingSystem.Area.User
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ParkingSpotController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IParkingSpotService _parkingSpotService;

        public ParkingSpotController( ApplicationDbContext context,IStringLocalizer<SharedResources> stringLocalizer,
            IParkingSpotService parkingSpotService)
        {
            _context = context;
            _stringLocalizer = stringLocalizer;
            _parkingSpotService = parkingSpotService;
        }
        [HttpGet("")]
        public IActionResult Index()
        {

            var responce = _parkingSpotService.GetSpotsAsync();
            return Ok(new { message = _stringLocalizer["Success"].Value, responce });
        }




    }
}
