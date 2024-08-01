using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudioUp.Models
{
    [Table("T_Trainings")]
    public class Training
    {
        
        public int ID { get; set; }

        [ForeignKey("TrainingType")]
        public int TrainingTypeID { get; set; }
        public virtual TrainingType TrainingType { get; set; }
        [ForeignKey("Trainers")]
        public int TrainerID { get; set; }
        public virtual Trainer Trainer { get; set; }
        public int DayOfWeek { get; set; }
        public TimeOnly Hour { get; set; }
        public int ParticipantsCount { get; set; }
        public bool IsActive { get; set; }

    }
}
