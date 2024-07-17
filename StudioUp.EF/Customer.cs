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

        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string Tel { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Address { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey("CustomerTypes")]
        public int CustomerTypeId { get; set; }
        public virtual CustomerType CustomerType { get; set; }
       
        [ForeignKey("HMOs")]
        public int HMOId { get; set; }
        public virtual HMO HMO { get; set; }
        
        [ForeignKey("PatmentOptions")]
        public int PaymentOptionId { get; set; }
        public virtual PaymentOption PaymentOption { get; set; }
        
        [ForeignKey("SubscriptionTypes")]
        public int SubscriptionTypeId { get; set; }
        public virtual SubscriptionType SubscriptionType { get; set; }
    }

   
       
        
       
}
