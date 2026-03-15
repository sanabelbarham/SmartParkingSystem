using DAL.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO.Responce
{
  public  class ParkingSpotResponce
    {
        public int ParkingSpotID { get; set; }

        public bool IsAvailable { get; set; } = true;

        public List<ParkingSpotTranslationResponce> Translations { get; set; }
    }
}
