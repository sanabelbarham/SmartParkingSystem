using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO.Responce.Registor
{
  public  class RegistorResponce
    {
        public string Message { get; set; }
        public List<string>? Errors { get; set; }

    }
}
