using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO.Request;
using DAL.DTO.Responce;
using DAL.Models;


namespace BLL.Service
{
  public  interface IParkingSpotService
    {
        Task <List<ParkingSpotResponce>>GetSpotsAsync();
        Task<ParkingSpotResponce> CreatParkingSpot(ParkingSpotRequest request);
        Task<BaseResponce> UpdateSpotAsync(int id,ParkingSpotRequest request);
        Task<BaseResponce> DeleteSpotAsync(int id);
        Task<List<ParkingSpotResponce>> GetAvaliableSpotsAsync();



    }
}