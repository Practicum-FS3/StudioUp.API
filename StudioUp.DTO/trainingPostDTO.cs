using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.DTO
{
    public class TrainingPostDTO
    {
            public int TrainerID { get; set; }
            public int DayOfWeek { get; set; }
            public int Hour { get; set; }
        public int Minutes { get; set; }
        public int TrainingCustomerTypeId { get; set; }
            public int ParticipantsCount { get; set; }
            public bool IsActive { get; set; }

 
    }
}
