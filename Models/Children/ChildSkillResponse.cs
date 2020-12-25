using System;

namespace WebApi.Models.Children
{
    public class ChildSkillResponse
    {
        public Guid ChildSkillId { get; set; }
        public string SkillName { get; set; }
        public DateTime SkillCreatedDate { get; set; }
        public DateTime SkillUpdatedDate { get; set; }
        public Guid ChildId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
