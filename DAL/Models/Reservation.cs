using DAL.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{

    public enum ReservationStatusEnum
    {
        Pending = 1,
        Reserved = 2,
        Expired = 3,
        Cancelled = 4,
        Completed = 5
    }


    public class Reservation
    {
        public int ReservationID { get; set; }
        //public int VehicleID { get; set; }
        //public int UserID { get; set; }
        //public int SpotID { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public DateTime ExpiryTime { get; set; }
        public ReservationStatusEnum? Status { get; set; }
  
        public DateTime CreatedAt { get; set; }
        public string ?Reservation_QR { get; set; }
        //****************************************

        //1 to many
        public string UserID { get; set; }
        public ApplicationUser User { get; set; }
        //1 to many
        public int VehicleID { get; set; }

        public Vehicle Vehicle { get; set; }
        // 1 to many
        public int ParkingSpotID { get; set; }
        public ParkingSpot ParkingSpot { get; set; }

        // 1 to 1
        public Payment? Payment { get; set; }
        

    }
}
