﻿namespace StudioUp.DTO
{
    public class ContentTypeDTO
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string LinkHP { get; set; }
        public string Link2 { get; set; }
        public string Title1 { get; set; }
        public string Title2 { get; set; }
        public string Title3 { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<ContentSectionDTO> ContentSections { get; set; }

    }
}
