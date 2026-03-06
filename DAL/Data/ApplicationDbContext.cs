using DAL.Identity;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
   public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {

  
        public DbSet<Payment> payments { get; set; }
        public DbSet<Vehicle> vehicles { get; set; }
        public DbSet<ParkingSpot> parkingSpots { get; set; }
        public DbSet<Reservation> reservations { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");



            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().HasMany(v=>v.vehicles)
                .WithOne(u=>u.User)
                .HasForeignKey(v=>v.UserID)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ParkingSpot>().HasMany(p=>p.reservations)
                .WithOne(r=>r.ParkingSpot).HasForeignKey(r=>r.ParkingSpotID)
                     .OnDelete(DeleteBehavior.NoAction);


            builder.Entity<Vehicle>().HasMany(p => p.Reservations)
               .WithOne(r => r.Vehicle).HasForeignKey(r => r.VehicleID)
                    .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ApplicationUser>().HasMany(p => p.reservations)
         .WithOne(r => r.User).HasForeignKey(r => r.UserID)
              .OnDelete(DeleteBehavior.NoAction);


            builder.Entity<ParkingSpot>().HasOne(p => p.QR)
         .WithOne(r => r.ParkingSpot)
         .HasForeignKey<QR>(r => r.ParkingSpotID)
              .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Reservation>().HasOne(p => p.Payment)
        .WithOne(r => r.Reservation)
        .HasForeignKey<Payment>(r => r.ReservationID)
             .OnDelete(DeleteBehavior.NoAction);









        }

    }
}
