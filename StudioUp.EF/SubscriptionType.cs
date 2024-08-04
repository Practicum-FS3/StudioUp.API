using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Models
{
    [Table("T_SubscriptionTypes")]
    public class SubscriptionType
    {
        public int ID { get; set; }
        [AllowNull]
        [MaxLength(50)]
        public string Title { get; set; }
        public bool IsActive { get; set; }
        [AllowNull]
        public int TotalTraining { get; set; }
        [AllowNull]
        public float PriceForTraining { get; set; }
        [AllowNull]
        public int NumberOfTrainingPerWeek { get; set; }
        [AllowNull]
        public string Description { get; set; }

    }
}
