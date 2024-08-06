using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.ComponentModel.DataAnnotations.Schema;
=======
>>>>>>> f3cfc56cc17eefcf57fbac36c02acf57c3bb8ded
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.DTO
{
    public class TrainingPostDTO
    {
<<<<<<< HEAD


        public int TrainerID { get; set; }
        public int DayOfWeek { get; set; }
        public int Hour { get; set; }
        public int Minutes { get; set; }
=======
        private string hour = "00";
        private string minute = "00";
        public int TrainerID { get; set; }
            public int DayOfWeek { get; set; }

        public string Hour
        {
            get { return hour; }
            set { hour = FormatToTwoDigits(value); }
        }

        public string Minute
        {
            get { return minute; }
            set { minute = FormatToTwoDigits(value); }
        }

>>>>>>> f3cfc56cc17eefcf57fbac36c02acf57c3bb8ded
        public int TrainingCustomerTypeId { get; set; }
        public int ParticipantsCount { get; set; }
        public bool IsActive { get; set; }

<<<<<<< HEAD
=======
        private string FormatToTwoDigits(string value)
        {
            if (int.TryParse(value, out int number) && number >= 0 && number <= 59)
            {
                return number.ToString("D2");
            }
            return "00";
        }
>>>>>>> f3cfc56cc17eefcf57fbac36c02acf57c3bb8ded
    }
}
