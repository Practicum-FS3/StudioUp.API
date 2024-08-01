using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using StudioUp.DTO;

namespace StudioUp.Models
{
    [Table("T_Trainings")]
    public class Training
    {
        public int ID { get; set; }

        [ForeignKey("TrainingCustomerType")]
        public int TrainingCustomerTypeId { get; set; }
        public virtual TrainingCustomerType TrainingCustomerType { get; set; }
        [ForeignKey("Trainers")]
        public int TrainerID { get; set; }
        public virtual Trainer Trainer { get; set; }
        public int DayOfWeek { get; set; }
        [ForeignKey("TrainingTime")]
        public int TimeId { get; set; }
        public virtual TrainingTime Time { get; set; }
        public int ParticipantsCount { get; set; }
        public bool IsActive { get; set; }

    }
}
