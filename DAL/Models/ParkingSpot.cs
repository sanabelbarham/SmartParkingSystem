using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
   public class ParkingSpot
    {
        public int ParkingSpotID { get; set; }
        public string SpotNumber { get; set; }
        public string ParkingFloor { get; set; }
        public string ParkingArea { get; set; }
        public bool IsAvailable { get; set; }
        public bool NearExit { get; set; }
        public QR QR { get; set; } = new QR();
        public List<Reservation> reservations { get; set; } = new List<Reservation>();
    }
    
}
