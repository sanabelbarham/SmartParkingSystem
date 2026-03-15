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


        public async Task<BaseResponce> DeleteSpotAsync(int id)
        {
            try
            {var parkingSpot = await _parkingSpotRepository.FindById(id);
                if (parkingSpot == null)
                {
                    return new BaseResponce
                    {
                        Message = "no parking spot with this id",
                        Success = false

                    };

                }
                else
                {

                    var result = _parkingSpotRepository.DeleteSpotAsync(parkingSpot);

                    return new BaseResponce
                    {


                        Success = true,
                        Message = "user deleted succeflluy"
                    };
                }
            }catch(Exception e)
            {
                return new BaseResponce
                {
                    Success = false,
                    Message = "delete not done",
                    Errors = new List<string> { e.Message }
                };
            }

            }

        public async Task<BaseResponce> UpdateSpotAsync(int id,ParkingSpotRequest request)
        {
            try
            {

                var spot = await _parkingSpotRepository.FindById(id);
                if (spot is null)
                {
                    return new BaseResponce
                    {
                        Message = "no parking spot with this id",
                        Success = false

                    };
                }
                else
{

                  

                    if (request.Translations != null && request.Translations.Any())
                    {
                        foreach (var translation in request.Translations)
                        {
                            var existingTranslation = spot.Translations
                                .FirstOrDefault(t => t.Language == translation.Language);

                            if (existingTranslation != null)
                            {
                                existingTranslation.SpotNumber = translation.SpotNumber;
                                existingTranslation.ParkingFloor = translation.ParkingFloor;
                                existingTranslation.ParkingArea = translation.ParkingArea;
                            }
                            else
                            {
                                return new BaseResponce
                                {
                                    Success = false,
                                    Message = $"Language {translation.Language} not supported"
                                };
                            }
                        }
                    }

                    await _parkingSpotRepository.UpdateSpotAsync(spot);


                    return new BaseResponce
                    {


                        Success = true,
                        Message = "user Updated succeflluy"
                    };
                }


            }catch(Exception e)
            {

                return new BaseResponce
                {
                    Success = false,
                    Message = "Update not done",
                    Errors = new List<string> { e.Message }
                };
            }
        }











    }


    }

