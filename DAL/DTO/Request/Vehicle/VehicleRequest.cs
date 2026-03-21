using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO.Request.Vehicle
{
   public class VehicleRequest
    {
        public string VehicleType { get; set; }//car, bus ...
        public string VehicleModel { get; set; }// the marka
        public string LicensePlate { get; set; }
        public string UserID { get; set; }
    }
}
