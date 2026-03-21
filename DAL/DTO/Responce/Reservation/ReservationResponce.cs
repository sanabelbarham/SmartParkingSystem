using DAL.Identity;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO.Responce.Reservation
{
    public class ReservationResponce
    {

        public int ReservationID { get; set; }
    

        public ReservationStatusEnum Status { get; set; }


        public string UserID { get; set; }
  
        public int VehicleID { get; set; }

        public int ParkingSpotID { get; set; }
    }
}
