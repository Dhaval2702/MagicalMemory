using System;
using System.Collections.Generic;

namespace WebApi.Models.Children
{
    public class ChildrenResponse
    {
        public Guid ChildId { get; set; }
        public string ChildName { get; set; }
        public DateTime ChildDOB { get; set; }
        public int BirthWeight { get; set; }
        public string ChildPhoto { get; set; }
        public int AccountId { get; set; }
        public string BloodGroup { get; set; }
        public string RelationShip { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<ChildSkillResponse> ChildSkillResponse { get; set; }
        public List<ChildMemoryResponse> ChildMemoryResponse { get; set; }
    }
}
