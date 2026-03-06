using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO.Request
{
    public class ParkingSpotRequest
    {
        public List<ParkingSpotTranslationRequest> Translations { get; set; }

    }
}
