using DAL.DTO.Request.Reservations;
using DAL.DTO.Responce;
using DAL.DTO.Responce.Checkout;
using DAL.DTO.Responce.Registor;
using DAL.DTO.Responce.Reservation;
using DAL.Identity;
using DAL.Migrations;
using DAL.Models;
using DAL.Repository;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service.Reservations
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManage;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReservationService (IReservationRepository ReservationRepository, IEmailSender emailSender,
            UserManager<ApplicationUser> userManage,
              IHttpContextAccessor httpContextAccessor
            )
        {
            _reservationRepository = ReservationRepository;
            _emailSender = emailSender;
            _userManage = userManage;
            _httpContextAccessor = httpContextAccessor;
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

                        var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                        request.UserID = currentUserId;
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
                                {"UserId",currentUserId}
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
            var userId = session.Metadata["UserId"];
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
           var user= await _userManage.FindByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("User not found");
            }


            Console.WriteLine("=== DEBUG ===");
            Console.WriteLine($"SessionId: {session_id}");
            Console.WriteLine($"Metadata UserId: {session.Metadata["UserId"]}");

            var reservation2 = await _reservationRepository.GetBySessionIdAsync(session_id);
            Console.WriteLine($"Reservation UserId: {reservation2?.UserID}");

            var user2 = await _userManage.FindByIdAsync(reservation2.UserID);
            Console.WriteLine($"User Email: {user2?.Email}");



            var random = new Random();
            var code = random.Next(200, 10000).ToString();
            reservation.EntryCode = code;
            reservation.EntryCodeExpiary = DateTime.UtcNow.AddMinutes(15);

            await _emailSender.SendEmailAsync(user.Email, "Payment Successfull", $"<h2> thank you your code is {code} and is valid for only 15 minutes </h2>");
            return new ReservationResponce
            {
                ReservationID = reservation.ReservationID,
                Status = (ReservationStatusEnum)reservation.Status,
              

                Message = "Payment successful, reservation confirmed",
                Success = true
            };

        
        }
    }
}
