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
    }
}
