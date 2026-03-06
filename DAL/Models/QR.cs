using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class QR
    {
        public int QRID { get; set; }
        public int ParkingSpotID { get; set; }
        public ParkingSpot ParkingSpot { get; set; }
    }
}
