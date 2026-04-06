using DAL.DTO.Request.Reservations;
using DAL.DTO.Responce;
using DAL.DTO.Responce.Checkout;
using DAL.DTO.Responce.Registor;
using DAL.DTO.Responce.Reservation;
using DAL.Models;
using DAL.Repository;
using Mapster;
using Stripe.Checkout;
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

        public async Task<CheckoutResponce> CreateReservationAsync(ReservationRequest reservationRequest)
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
                    return new CheckoutResponce
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
                    return new CheckoutResponce
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
                        return new CheckoutResponce
                        {
                            Message = $"Reserved completeed plz pay cash, your total price is {  request.TotalPrice}  doller ",
                            Success = true,

                        };

                    }

                    else if (request.PaymentMethod == PaymentMethodEnum.Visa)
                    {

                      //  var spot = await _reservationRepository.GetParkingSpotById(request.ParkingSpotID);
                        

                        var options = new SessionCreateOptions
                        {
                            PaymentMethodTypes = new List<string> { "card" },
                            LineItems = new List<SessionLineItemOptions>
                        {
                            new SessionLineItemOptions
                            {
                                PriceData = new SessionLineItemPriceDataOptions
                                {
                                    Currency = "USD",
                                    ProductData = new SessionLineItemPriceDataProductDataOptions
                                    {
                                        Name = "Parking spot",
                         
                                    },
                                    UnitAmount = (long)request.TotalPrice*100,
                                },
                                Quantity = 1,
                            },
                        },
                            Mode = "payment",
                            SuccessUrl = $"https://localhost:7250/api/reservations/success?session_id={{CHECKOUT_SESSION_ID}}",
                            CancelUrl = $"https://localhost:7250/api/reservations/cancle",
                            Metadata=new Dictionary<string, string>
                            {
                                {"UserId",request.UserID}
                            }
                        };
                        var service = new SessionService();
                        var session = service.Create(options);

                        request.SessionId = session.Id;
                        request.PaymentStatus = PaymentStatusEnum.Pending;

                        await _reservationRepository.CreateReservationAsync(request);
                     

                        return new CheckoutResponce
                        {
                            Message = "session is created to pay with visa ",
                            Success = true,
                            Url=session.Url

                        };
                    }
                    else
                        return new CheckoutResponce
                        {
                            Message = "plz choose a correct payment method ",
                            Success = false,

                        };

                }


                return new CheckoutResponce
                {
                    Message = "The user id or vichle id or parking spot id is not found",
                    Success = false,


                };
            }catch(Exception e)
            {
                return new CheckoutResponce
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

        public async Task<ReservationResponce> HandleSuccessAsync(string session_id)
        {
            var service = new SessionService();
            var session = service.Get(session_id);

            //  Verify payment
            if (!string.Equals(session.PaymentStatus, "paid", StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception("Payment not completed");
            }

            // Get reservation from DB
            var reservation = await _reservationRepository.GetBySessionIdAsync(session_id);

            if (reservation == null)
            {
                throw new Exception("Reservation not found");
            }

            //  Update reservation
            reservation.Status = ReservationStatusEnum.Reserved;
            reservation.PaymentStatus = PaymentStatusEnum.Paid;
            reservation.PaymentId = session.PaymentIntentId;

            await _reservationRepository.UpdateReservationAsync(reservation);

            //Lock parking spot
            var spot = await _reservationRepository.GetParkingSpotById(reservation.ParkingSpotID);
            if (spot == null)
            {
                throw new Exception("Parking spot not found");
            }


            spot.IsAvailable = false;

            await _reservationRepository.UpdateParkingSpotAsync(spot);

            //return response
            return new ReservationResponce
            {
                ReservationID = reservation.ReservationID,
                Status = (ReservationStatusEnum)reservation.Status,            
                Message = "Payment successful, reservation confirmed",
                Success=true
            };
        }
    }
}
