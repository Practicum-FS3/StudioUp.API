using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace StudioUp.Models
{
    [Table("T_CustomerSubscription")]
    public class CustomerSubscription
    {
        [Key]
        public int ID { get; set; }

        [AllowNull]
        [ForeignKey("Customer")]
        public int CustomerID { get; set; }

        [AllowNull]
        [ForeignKey("SubscriptionType")]
        public int SubscriptionTypeId { get; set; }

        [AllowNull]
        public DateTime StartDate { get; set; }

        // קשרי גומלין אופציונליים עם טבלאות אחרות
        public virtual Customer Customer { get; set; }
        public virtual SubscriptionType SubscriptionType { get; set; }

        public bool IsActive { get; set; }
    }
}
