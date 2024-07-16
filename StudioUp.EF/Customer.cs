using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Models
{
    [Table("T_Customers")]
    public class Customer
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string LastName { get; set; }
        public int CustomerTypeId { get; set; }
        public int HMOId { get; set; }
        public int PaymentOptionsId { get; set; }
        public int SubscriptionTypeId { get; set; }
        //public bool IsActive { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string Tel { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Adress { get; set; }

        //relationShip
        public CustomerType CustomerType { get; set; }
        public HMO HMO { get; set; }
        public PaymentOption PaymentOption { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
    }
}
