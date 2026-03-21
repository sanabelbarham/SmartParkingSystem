using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO.Request.Reservations
{
  public  class ReservationRequest
    {
   
  
        public string UserID { get; set; }
        public int VehicleID { get; set; }
        public int ParkingSpotID { get; set; }
    }
}
