using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.DTO
{
    public class AvailableTrainingDTO
    {
        public int Id { get; set; }
        public int TrainingId { get; set; }
        public DateOnly Date { get; set; }
        public int ParticipantsCount { get; set; }
        public bool IsActive { get; set; }

        public AvailableTrainingDTO(int TrainingId, DateOnly Date,int ParticipantsCount, bool IsActive)
        {
            this.TrainingId = TrainingId;   
            this.Date = Date;
            this.ParticipantsCount = ParticipantsCount;
            this.IsActive = IsActive;
        }

    }
}
