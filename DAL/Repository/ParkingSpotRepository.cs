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

        public async Task<ParkingSpot> CreateSpot(ParkingSpot parkingSpot)
        {
            _context.parkingSpots.Add(parkingSpot);
            _context.SaveChanges();
            return parkingSpot;
        }

        public async Task DeleteSpotAsync(ParkingSpot spot)
        {
          _context.parkingSpots.Remove(spot);
            _context.SaveChanges();



        }

        public async Task<ParkingSpot> FindById(int id)
        {
          return await _context.parkingSpots.Include(c=>c.Translations).FirstOrDefaultAsync(c=>c.ParkingSpotID==id);


        }

        public async Task<List<ParkingSpot>> PrintAsync()
        {

           var spots=   _context.parkingSpots.Include(c => c.Translations).ToList();
      
            return spots;
        }



        public async Task<List<ParkingSpot>> GetAvailableSpotsAsync()
        {

            var spots = _context.parkingSpots.Include(c => c.Translations).Where(c=>c.IsAvailable==true).ToList();

            return spots;
        }

        public async Task UpdateSpotAsync(ParkingSpot spot)
        {
            _context.parkingSpots.Update(spot);
            _context.SaveChanges();
        }
    }
}
