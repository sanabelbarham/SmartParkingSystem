using DAL.DTO.Request.Reservations;
using DAL.DTO.Responce;
using DAL.DTO.Responce.Reservation;
using DAL.Models;
using DAL.Repository;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service.Reservations
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService (IReservationRepository ReservationRepository)
        {
            _reservationRepository = ReservationRepository;
        }

        public async Task<BaseResponce> CreateReservationAsync(ReservationRequest reservationRequest)
        {

            

            var request = reservationRequest.Adapt<Reservation>();
            request.EntryTime = DateTime.UtcNow;
            request.ExpiryTime = DateTime.UtcNow.AddMinutes(15);
            request.Status = ReservationStatusEnum.Reserved;
             await _reservationRepository.CreateReservationAsync(request);
            return new BaseResponce
            {
                Message = "Reserved completeed",
                Success = true,

            };

        }

        public async Task<BaseResponce> DeleteReservationAsync(ReservationRequest reservationRequest)
        {


            var request = reservationRequest.Adapt<Reservation>();
           
            await _reservationRepository.DeleteReservationAsync(request);
            return new BaseResponce
            {
                Message = "Reserved deleted",
                Success = true,

            };
        }

        public  async Task<List<ReservationResponce>> GetReservationAsync()
        {
            var responce = await _reservationRepository.GetReservationAsync();
            var result = responce.Adapt<List<ReservationResponce>>();
            return result;
        }
    }
}
