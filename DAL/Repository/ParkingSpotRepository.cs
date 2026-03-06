using DAL.Data;
using DAL.DTO.Responce;
using DAL.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class ParkingSpotRepository : IParkingSpotRepository
    {
        private readonly ApplicationDbContext _context;

        public ParkingSpotRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ParkingSpot>> PrintAsync()
        {

           var spots= _context.parkingSpots.Include(c => c.Translations).ToList();
      
            return spots;
        }
    }
}
