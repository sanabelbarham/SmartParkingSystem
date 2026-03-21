using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ApplicationDbContext _context;

        public VehicleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateVehicle(Vehicle vehicle)
        {

           var result= _context.vehicles.Add(vehicle);
            _context.SaveChanges();
   
        }

       

        public async Task DeleteVehicleAsync(Vehicle spot)
        {
            _context.vehicles.Remove(spot);
            _context.SaveChanges();
        }

        public async Task<Vehicle> FindById(int id)
        {
            return await _context.vehicles.FirstOrDefaultAsync(c => c.VehicleID == id);

        }

        public async Task UpdateVehicleAsync(Vehicle vehicle)
        {

            _context.vehicles.Update(vehicle);
            _context.SaveChanges();
        }
    }
}
