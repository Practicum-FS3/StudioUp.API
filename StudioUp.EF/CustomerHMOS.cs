using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Models
{
    [Table("T_CustomerHMOS")]
    public class CustomerHMOS
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        public int HMOID { get; set; }
        public virtual HMO HMOs { get; set; }
        [Column(TypeName = "nvarchar(15)")]
        public string FreeFitId { get; set; }
    }
}
