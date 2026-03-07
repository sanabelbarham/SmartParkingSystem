using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO.Request.Registor
{
   public class RegistorRequests
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string City { get; set; }
    }
}
