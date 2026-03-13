using DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Identity
{
   public class ApplicationUser:IdentityUser
    {

      //identity by default gives you id, email,hashpassword,phone number
        public string FullName { get; set; }
   
        public bool? BlockedStatus { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string ? PaswordResetCode { get; set; }

        public DateTime? PaswordResetCodeExpiary { get; set; }

        public List<Vehicle> vehicles { get; set; } = new List<Vehicle>();
        public List<Reservation> reservations { get; set; } = new List<Reservation>();

    }
}
