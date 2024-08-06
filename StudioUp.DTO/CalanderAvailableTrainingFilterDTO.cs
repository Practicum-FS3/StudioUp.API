using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.DTO
{
    public class CalanderAvailableTrainingFilterDTO
    {
        public bool? Past;
        public bool? Future;
        public DateOnly? StratDate { get; set; }
        public DateOnly? EndDate { get; set; }
    }
}
