using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Models
{
    [Table("T_TrainingsCustomers")]
    public class TrainingCustomer
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("AvailableTrainings")]
        public int TrainingID { get; set; }
        public virtual AvailableTraining Training { get; set; }
        [ForeignKey("Customer")]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        public bool Attended { get; set; }
        //public bool IsActive { get; set; }


    }

}
