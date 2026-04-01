using DAL.DTO.Request.Reservations;
using DAL.DTO.Responce;
using DAL.DTO.Responce.Registor;
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
            try
            {


                // await _reservationRepository.CreateReservationAsync(request);

                var request = reservationRequest.Adapt<Reservation>();

                request.EntryTime = DateTime.UtcNow;
         
                request.ExpiryTime = DateTime.UtcNow.AddMinutes(15);
                request.Status = ReservationStatusEnum.Pending;
          
                if (request.ExitTime <= request.EntryTime)
                {
                    return new BaseResponce
                    {
                        Success = false,
                        Message = "Exit time must be greater than entry time"
                    };
                }
                request.CreatedAt = DateTime.UtcNow;
                TimeSpan Duration = request.ExitTime - request.EntryTime;
                request.TotalPrice = Duration.TotalHours * 1.0;//i doller per houre 

                var result = await _reservationRepository.IsParkingSpotReserved(request.ParkingSpotID);
                //if there is reservation dont do any thing
                if (result == true)
                {
                    return new BaseResponce
                    {
                        Message = " parking spot id  is reserved or pending",
                        Success = false,

                    };

                }

                var userExists = await _reservationRepository.FindIfUserexis(request.UserID);
                var vichleExists = await _reservationRepository.FindIfVichleexis(request.VehicleID);
                var ParkingSpotExists = await _reservationRepository.FindIfParkingSpotexis(request.ParkingSpotID);
                if (userExists && vichleExists && ParkingSpotExists)
                {

                    if (request.PaymentMethod == PaymentMethodEnum.Cash)
                    {

                        await _reservationRepository.CreateReservationAsync(request);
                        return new BaseResponce
                        {
                            Message = $"Reserved completeed plz pay cash, your total price is {  request.TotalPrice}  doller ",
                            Success = true,

                        };

                    }
                    else if (request.PaymentMethod == PaymentMethodEnum.Visa)
                    {

                        //I will do somth this is not done yet
                        return new BaseResponce
                        {
                            Message = "Reserved completeed, visa is accepted ",
                            Success = true,

                        };
                    }
                    else
                        return new BaseResponce
                        {
                            Message = "plz choose a correct payment method ",
                            Success = false,

                        };

                }


                return new BaseResponce
                {
                    Message = "The user id or vichle id or parking spot id is not found",
                    Success = false,


                };
            }catch(Exception e)
            {
                return new BaseResponce
                {
                    Message = "An UnExpected error",
                    Success = false,
                    Errors = new List<string> { e.Message }
                };
            }

        }

        public async Task<BaseResponce> DeleteReservationAsync( int id)
        {

            try
            {
                //   var request = reservationRequest.Adapt<Reservation>();

                var result = await _reservationRepository.FindById(id);
                if (result == null)
                {
                    return new BaseResponce
                    {
                        Message = " no reservation with this id",
                        Success = false,

                    };
                }
                await _reservationRepository.DeleteReservationAsync(result);
                return new BaseResponce
                {
                    Message = "Reserved deleted",
                    Success = true,

                };
            }
            catch(Exception e)
            {
                return new BaseResponce
                {
                    Message = "An UnExpected error",
                    Success = false,
                    Errors = new List<string> { e.Message }

                };
            }
        }

        public  async Task<List<ReservationResponce>> GetReservationAsync()
        {
            
                var responce = await _reservationRepository.GetReservationAsync();
                var result = responce.Adapt<List<ReservationResponce>>();
                return result;
        
         
        }
    }
}
