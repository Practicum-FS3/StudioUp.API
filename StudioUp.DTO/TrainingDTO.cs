using System;
using System.Collections.Generic;
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
        public TrainingTimeDTO Time { get; set; }
        public int TrainingCustomerTypeId { get; set; }
        public int ParticipantsCount { get; set; }
        public bool IsActive { get; set; }

    }
}
