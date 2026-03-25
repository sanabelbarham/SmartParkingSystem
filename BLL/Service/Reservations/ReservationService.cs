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
            request.Status = ReservationStatusEnum.Pending;

             var result =await _reservationRepository.FindReservation(request);
            //if there is reservation dont do any thing
            if(result ==true)
            {
                return new BaseResponce
                {
                    Message = "there is a reservation  with these info",
                    Success = false,

                };

            }

            var userExists = await _reservationRepository.FindIfUserexis(request.UserID);
            var vichleExists = await _reservationRepository.FindIfVichleexis(request.VehicleID);
            var ParkingSpotExists = await _reservationRepository.FindIfParkingSpotexis(request.ParkingSpotID);
            if(userExists && vichleExists && ParkingSpotExists)
            {

                await _reservationRepository.CreateReservationAsync(request);

                return new BaseResponce
                {
                    Message = "Reserved completeed",
                    Success = true,

                };

            }


            return new BaseResponce
            {
                Message = "The user id or vichle id or parking spot id is not found",
                Success = false,

            };

        }

        public async Task<BaseResponce> DeleteReservationAsync( int id)
        {


         //   var request = reservationRequest.Adapt<Reservation>();
           
           var result= await _reservationRepository.FindById(id);
            if(result == null)
            {
                return new BaseResponce
                {
                    Message = " no reservation with this id",
                    Success = true,

                };
            }
            await _reservationRepository.DeleteReservationAsync(result);
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
