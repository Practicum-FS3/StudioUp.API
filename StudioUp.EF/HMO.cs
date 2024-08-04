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
    [Table("T_HMOs")]
    public class HMO
    {
        public int ID { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        public bool IsActive { get; set; }
        [AllowNull]
        public string ArrangementName { get; set; }
        [AllowNull]
        public int TrainingsPerMonth { get; set; }
        [AllowNull]
        public double TrainingPrice { get; set; }
        [AllowNull]
        public double MinimumAge { get; set; }
        [AllowNull]
        public double MaximumAge { get; set; }
        [AllowNull]
        public string TrainingDescription { get; set; }
    }
}
