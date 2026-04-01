using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DAL.DTO.Request.Reservations
{
  public  class ReservationRequest
    {

       [JsonConverter(typeof(JsonStringEnumConverter))]
        public string UserID { get; set; }
        public int VehicleID { get; set; }
        public int ParkingSpotID { get; set; }

        public DateTime ExitTime { get; set; }

        public PaymentMethodEnum PaymentMethod { get; set; }


    }
}
