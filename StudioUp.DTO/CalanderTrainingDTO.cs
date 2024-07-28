using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.DTO
{
    public class CalanderTrainingDTO
    {
        public int ID { get; set; }
        public int TrainerID { get; set; }
        public string  TrainerName { get; set; }
        public int DayOfWeek { get; set; }
        public string Hour { get; set; }
        //public string Minutes { get; set; }
        public bool IsActive { get; set; }
        //ID,hour,description,trainerName
    }
}
