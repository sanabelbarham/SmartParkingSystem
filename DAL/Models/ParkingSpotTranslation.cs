using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
  public  class ParkingSpotTranslation
    {
        public int Id { get; set; }
        public string Language { get; set; } = "en";
        public string SpotNumber { get; set; }//A-10
        public string ParkingFloor { get; set; }
        public string ParkingArea { get; set; }
        public int ParkingSpotId { get; set; }
        public ParkingSpot ParkingSpot { get; set; }

    }
}
