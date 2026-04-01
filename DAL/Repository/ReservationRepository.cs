using DAL.Data;
using DAL.DTO.Request.Reservations;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ApplicationDbContext _context;

        public ReservationRepository( ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateReservationAsync(Reservation reservationRequest)
        { 
           _context.reservations.Add(reservationRequest);
            _context.SaveChanges();



        }
        public async Task<Reservation> FindById(int id)
        {
            return await _context.reservations.FirstOrDefaultAsync(c => c.ReservationID == id);
        }

        public async Task DeleteReservationAsync(Reservation reservationRequest)
        {
            _context.reservations.Remove(reservationRequest);
            _context.SaveChanges();
        }

        public async Task <List<Reservation>>  GetReservationAsync()
        {
            var result = _context.reservations.Include(c=>c.Vehicle).
                Include(c=>c.User).Include(c=>c.ParkingSpot).ToList();
               return result;

        }

        public async Task<bool> IsParkingSpotReserved(int  ParkingId)
        {
            var result = await _context.reservations.

                AnyAsync
                (c=>c.ParkingSpotID== ParkingId
                &&( c.Status ==ReservationStatusEnum.Pending || c.Status==ReservationStatusEnum.Reserved)
            );
            return result;
        }

        public async Task<bool> FindIfUserexis(string id)
        {
            var result = await _context.Users.FirstOrDefaultAsync(c => c.Id == id);
            if (result is null)
                return false;
            return true;
        }

        public async Task<bool> FindIfVichleexis(int id)
        {
      


            var result = await _context.vehicles.FirstOrDefaultAsync(c => c.VehicleID == id);
            if (result is null)
                return false;
            return true;
        }

        public  async Task<bool> FindIfParkingSpotexis(int id)
        {

            var result = await _context.parkingSpots.FirstOrDefaultAsync(c => c.ParkingSpotID == id);
            if (result is null)
                return false;
            return true;
        }
    }
}
