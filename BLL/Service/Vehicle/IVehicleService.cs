using DAL.DTO.Request;
using DAL.DTO.Request.Vehicle;
using DAL.DTO.Responce;
using DAL.DTO.Responce.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service.Vehicle
{
   public interface IVehicleService
    {

        Task<VehicleResponce> CreateVehicleAsync(VehicleRequest vehicleRequest);
        
       Task<BaseResponce> UpdateVehicleAsync(int id, VehicleRequest request);
        Task<BaseResponce> DeleteVehicleAsync(int id);

    }
}
