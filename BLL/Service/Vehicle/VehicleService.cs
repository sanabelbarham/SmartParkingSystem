using DAL.DTO.Request.Vehicle;
using DAL.DTO.Responce.Vehicle;
using Mapster;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repository;
using DAL.DTO.Responce;
using System.Numerics;

namespace BLL.Service.Vehicle
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<VehicleResponce> CreateVehicleAsync(VehicleRequest vehicleRequest)
        {


            var vehicle = vehicleRequest.Adapt<DAL.Models.Vehicle>();
            await _vehicleRepository.CreateVehicle(vehicle);

            return new VehicleResponce
            {
                Success = true,
                Message = "Vehicle created successfuly"
            };

        }

        public async Task<BaseResponce> DeleteVehicleAsync(int id)
        {
            try
            {
                var Vehicle = await _vehicleRepository.FindById(id);
                if (Vehicle == null)
                {
                    return new BaseResponce
                    {
                        Message = "no Vehicle with this id",
                        Success = false

                    };

                }
                else
                {

                    var result = _vehicleRepository.DeleteVehicleAsync(Vehicle);

                    return new BaseResponce
                    {


                        Success = true,
                        Message = "Vehicle deleted succeflluy"
                    };
                }
            }
            catch (Exception e)
            {
                return new BaseResponce
                {
                    Success = false,
                    Message = "delete not done",
                    Errors = new List<string> { e.Message }
                };
            }
        }

        public async Task<BaseResponce> UpdateVehicleAsync(int id, VehicleRequest request)
        {
            try
            {

                var spot = await _vehicleRepository.FindById(id);
                if (spot is null)
                {
                    return new BaseResponce
                    {
                        Message = "no Vehicle with this id",
                        Success = false

                    };
                }
                else
                {
                    var result = request.Adapt<DAL.Models.Vehicle>();
                    await _vehicleRepository.UpdateVehicleAsync(result);
                    return new BaseResponce
                    {
                        Message = "Updated",
                        Success = true

                    };
                }
            }
            catch (Exception e)
            {
                return new BaseResponce
                {
                    Success = false,
                    Message = "Update not done"
                };
            }
        }

      
    }
}
