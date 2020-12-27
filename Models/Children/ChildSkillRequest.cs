using System;

namespace WebApi.Models.Children
{
    public class ChildSkillRequest
    {
        public string SkillName { get; set; }
        public DateTime SkillCreatedDate { get; set; }
        public DateTime? SkillUpdatedDate { get; set; }
    }
}
