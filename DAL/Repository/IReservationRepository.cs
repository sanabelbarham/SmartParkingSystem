using Azure.Core;
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
        Task <bool> IsParkingSpotReserved(int  ParkingId);
        Task <bool> FindIfUserexis(string id );
        Task <bool> FindIfVichleexis(int id );
        Task <bool> FindIfParkingSpotexis(int id );
        Task <List<Reservation>> GetReservationAsync();
        Task<Reservation> FindById(int id);
        Task<ParkingSpot> GetParkingSpotById(int parkingSpotID);

        Task<Reservation?> GetBySessionIdAsync(string sessionId);
        Task UpdateReservationAsync(Reservation reservation);


        Task UpdateParkingSpotAsync(ParkingSpot spot);
    }
}
