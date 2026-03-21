using DAL.DTO.Request.Reservations;
using DAL.DTO.Responce;
using DAL.DTO.Responce.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service.Reservations
{
 public   interface IReservationService
    {
        Task<BaseResponce> CreateReservationAsync(ReservationRequest reservationRequest);
        Task<BaseResponce> DeleteReservationAsync(ReservationRequest reservationRequest);
        Task<List<ReservationResponce>> GetReservationAsync( );
    }
}
