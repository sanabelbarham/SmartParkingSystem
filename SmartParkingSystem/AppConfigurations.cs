using BLL.Service.Authentication;
using BLL.Service;
using DAL.Repository;
using DAL.Utilis;
using Microsoft.AspNetCore.Identity.UI.Services;


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
            Services.AddTransient<BLL.Service.Authentication.IEmailSender, EmailSender>();

        }
        }
}
