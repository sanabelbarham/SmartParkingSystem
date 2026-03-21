using BLL.Service.Authentication;
using BLL.Service;
using DAL.Repository;
using DAL.Utilis;
using Microsoft.AspNetCore.Identity.UI.Services;
using BLL.Service.Vehicle;
using BLL.Service.Reservations;


namespace SmartParkingSystem
{
    public  static class AppConfigurations
    {

        public static void Config(IServiceCollection Services)
        {


            Services.AddScoped<IParkingSpotService, ParkingSpotService>();
            Services.AddScoped<IParkingSpotRepository, ParkingSpotRepository>();
            Services.AddScoped<ISeedData, SeedData>();
            Services.AddScoped<ISeedData, UserSeedData>();
            Services.AddScoped<IAuthenticationService, AuthenticationService>();
            Services.AddScoped<IVehicleService, VehicleService>();
            Services.AddScoped<IVehicleRepository, VehicleRepository>();
            Services.AddScoped<IReservationService, ReservationService>();
            Services.AddScoped<IReservationRepository, ReservationRepository>();
            Services.AddTransient<BLL.Service.Authentication.IEmailSender, EmailSender>();

        }
        }
}
