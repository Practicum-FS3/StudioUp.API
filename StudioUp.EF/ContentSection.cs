using System.ComponentModel.DataAnnotations.Schema;

namespace StudioUp.Models
{
    public class ContentSection
    {
        public int ID { get; set; }
        public string Section1 { get; set; }
        public string Section2 { get; set; }
        public string Section3 { get; set; }
        public bool IsActive { get; set; }
        public bool ViewInHP { get; set; }

        [ForeignKey("ContentType")]
        public int ContentTypeID { get; set; }
        public virtual ContentType ContentType { get; set; }
    }
}
