using DAL.Identity;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
   public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DbSet<Payment> payments { get; set; }
        public DbSet<Vehicle> vehicles { get; set; }
        public DbSet<ParkingSpotTranslation> Translations { get; set; }
        public DbSet<ParkingSpot> parkingSpots { get; set; }
        public DbSet<Reservation> reservations { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
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
        public override int SaveChanges()
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var entries = ChangeTracker.Entries<BaseModel>();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property(x => x.CreatedBy).CurrentValue = currentUserId;
                    entry.Property(x => x.CreatedAt).CurrentValue = DateTime.UtcNow;
                }

                else if (entry.State == EntityState.Modified)
                {
                    entry.Property(x => x.UpdatedBy).CurrentValue = currentUserId;
                    entry.Property(x => x.UpdatedAt).CurrentValue = DateTime.UtcNow;
                }
            }
            return base.SaveChanges();

        }

       
    }
}
