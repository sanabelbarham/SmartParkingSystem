using DAL.DTO.Request.Reservations;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
   public interface IReservationRepository
    {
        Task CreateReservationAsync(Reservation reservationRequest);
        Task DeleteReservationAsync(Reservation reservationRequest);
        Task <bool> FindReservation(Reservation reservationRequest);
        Task <bool> FindIfUserexis(string id );
        Task <bool> FindIfVichleexis(int id );
        Task <bool> FindIfParkingSpotexis(int id );
        Task <List<Reservation>> GetReservationAsync();
        Task<Reservation> FindById(int id);
    }
}
