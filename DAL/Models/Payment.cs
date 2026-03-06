using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{

    public enum PaymentMethodEnum{
        Cash=1,
        Visa=2
    }

    public enum PaymentStatusEnum
    {
        Paid = 1,
        UnPaid = 2
    }

    public  class Payment
    {
        public int PaymentID { get; set; }
        public double Amount { get; set; }
        public DateTime PaidAt { get; set; }
        public PaymentStatusEnum PaymentStatus { get; set; }
        public PaymentMethodEnum PaymentMethod { get; set; }
        public int ReservationID { get; set; }

        public Reservation Reservation { get; set; }
    }
}
