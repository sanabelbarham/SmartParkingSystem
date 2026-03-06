using DAL.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Vehicle
    {
        public int VehicleID { get; set; }
        public string VehicleType { get; set; }
        public string VehicleModel { get; set; }
        public string LicensePlate { get; set; }
        public string UserID { get; set; }
        public ApplicationUser User { get; set; }
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
