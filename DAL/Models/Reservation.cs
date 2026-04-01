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

    public enum PaymentMethodEnum
    {
        Cash = 1,
        Visa = 2
    }

    public enum PaymentStatusEnum
    {
        Paid = 1,
        UnPaid = 2
    }


    public class Reservation
    {
        public int ReservationID { get; set; }
        //public int VehicleID { get; set; }
        //public int UserID { get; set; }
        //public int SpotID { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime ExitTime { get; set; }
    
        public double TotalPrice { get; set; }
        public DateTime ExpiryTime { get; set; }
        //public decimal Price { get; set; }
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
        public PaymentStatusEnum PaymentStatus { get; set; } = PaymentStatusEnum.UnPaid;
        public PaymentMethodEnum PaymentMethod { get; set; }



    }
}
