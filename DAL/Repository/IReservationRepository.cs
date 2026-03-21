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
        Task <List<Reservation>> GetReservationAsync();
    }
}
