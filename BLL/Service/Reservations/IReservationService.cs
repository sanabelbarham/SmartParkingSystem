using DAL.DTO.Request.Reservations;
using DAL.DTO.Responce;
using DAL.DTO.Responce.Checkout;
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
        Task<CheckoutResponce> CreateReservationAsync(ReservationRequest reservationRequest);
        Task<BaseResponce> DeleteReservationAsync(int id);
        Task<List<ReservationResponce>> GetReservationAsync( );
    }
}
