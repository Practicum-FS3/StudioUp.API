using StudioUp.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace StudioUp.Models
{
    [Table("T_TrainigTypes")]
    public class TrainingType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        [ForeignKey("CustomerType")]
        public int CustomerTypeID { get; set; }
        public virtual CustomerType CustomerType { get; set; }
    }
}

