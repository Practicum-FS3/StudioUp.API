using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Models
{
    [Table("T_CustomerHMOS")]
    public class CustomerHMOS
    {
        public int ID { get; set; }
        [AllowNull]
        [ForeignKey("customer")]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        [AllowNull]
        [ForeignKey("HMOs")]
        public int HMOID { get; set; }
        public virtual HMO HMOs { get; set; }
        [Column(TypeName = "nvarchar(15)")]
        [AllowNull]
        public string FreeFitId { get; set; }

        public bool IsActive { get; set; }
    }
}
