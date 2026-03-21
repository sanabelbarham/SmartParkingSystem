using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
 public   interface IVehicleRepository
    {
        Task CreateVehicle(Vehicle vehicle);
        Task DeleteVehicleAsync(Vehicle vehicle);
        Task UpdateVehicleAsync(Vehicle vehicle);
        Task<Vehicle> FindById(int id);
    }

}
