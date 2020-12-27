using System;
using System.Collections.Generic;

namespace WebApi.Models.Children
{
    public class ChildrenRequest
    {
        public string ChildName { get; set; }
        public DateTime ChildDOB { get; set; }
        public int BirthWeight { get; set; }
        public string ChildPhoto { get; set; }
        public int AccountId { get; set; }
        public string BloodGroup { get; set; }
        public string RelationShip { get; set; }
        public List<ChildSkillRequest> ChildSkillRequest { get; set; }
        public List<ChildMemoryRequest> ChildMemoryRequest { get; set; }

    }
}
