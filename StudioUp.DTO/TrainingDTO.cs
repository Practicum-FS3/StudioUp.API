using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.DTO
{
    public class TrainingDTO
    {
        public int ID { get; set; }
        //public int TrainingTypeID { get; set; }
        //public int CustomerTypeID { get; set; }
       // public int TrainingCustomerTypeID {  get; set; }
        public int TrainerID { get; set; }
        public int DayOfWeek { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int TrainingCustomerTypeId { get; set; }
        public string TrainingCustomerTypeName { get; set; }
        public int ParticipantsCount { get; set; }
        public bool IsActive { get; set; }

    }
}
