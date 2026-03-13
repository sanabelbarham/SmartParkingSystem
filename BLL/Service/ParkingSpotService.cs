using DAL.DTO.Request;
using DAL.DTO.Responce;
using DAL.Models;
using DAL.Repository;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class ParkingSpotService : IParkingSpotService
    {
        private readonly IParkingSpotRepository _parkingSpotRepository;

        public ParkingSpotService(IParkingSpotRepository parkingSpotRepository)
        {
            _parkingSpotRepository = parkingSpotRepository;
        }

        public async Task<ParkingSpotResponce> CreatParkingSpot(ParkingSpotRequest request)
        {
            var parkingSpot = request.Adapt<ParkingSpot>();
           var result= await _parkingSpotRepository.CreateSpot(parkingSpot);
            var responce = result.Adapt<ParkingSpotResponce>();
            return responce;
        }

        public async Task<List<ParkingSpotResponce>> GetSpotsAsync()
        {
            var spots = await _parkingSpotRepository.PrintAsync();
            var responce = spots.Adapt<List<ParkingSpotResponce>>();


            return responce;
        }

    }
}
