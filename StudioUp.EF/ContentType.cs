
using System.Diagnostics.CodeAnalysis;

namespace StudioUp.Models
{
    public class ContentType
    {
        public int ID { get; set; }
        [AllowNull]
        public string Title { get; set; }
        [AllowNull]
        public string Description { get; set; }
        [AllowNull]
        public string LinkHP { get; set; }
        [AllowNull]
        public string Link2 { get; set; }
        [AllowNull]
        public string Title1 { get; set; }
        [AllowNull]
        public string Title2 { get; set; }
        [AllowNull]
        public string Title3 { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<ContentSection> ContentSections { get; set; }
    }
}

