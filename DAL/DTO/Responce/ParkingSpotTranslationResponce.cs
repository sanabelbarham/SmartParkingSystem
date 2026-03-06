using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO.Responce
{
   public class ParkingSpotTranslationResponce
    {

        public string Language { get; set; } = "en";
        public string SpotNumber { get; set; }//A-10
        public string ParkingFloor { get; set; }
        public string ParkingArea { get; set; }
    }
}
