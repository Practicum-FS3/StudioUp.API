using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.DTO
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CustomerTypeId { get; set; }
        public int HMOId { get; set; }
        public int PaymentOptionsId { get; set; }
        public int SubscriptionTypeId { get; set; }
        //public bool IsActive { get; set; }
        public string Tel { get; set; }
        public string Adress { get; set; }
    }
}
