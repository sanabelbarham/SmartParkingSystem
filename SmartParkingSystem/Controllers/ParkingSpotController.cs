using DAL.Data;
using DAL.DTO.Responce;
using DAL.Migrations;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SmartParkingSystem.Resources;

namespace SmartParkingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingSpotController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public ParkingSpotController( ApplicationDbContext context,IStringLocalizer<SharedResources> stringLocalizer)
        {
            _context = context;
            _stringLocalizer = stringLocalizer;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            var spots = _context.parkingSpots.Include(c=>c.Translations).ToList();
            var responce = spots.Adapt<List<ParkingSpotResponce>>();
            return Ok(new{ message=_stringLocalizer["Success"].Value,responce});
        }
    }
}
