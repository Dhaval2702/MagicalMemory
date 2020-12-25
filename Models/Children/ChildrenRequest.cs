using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Children
{
    public class ChildrenRequest
    {
        public string ChildName { get; set; }
        public DateTime ChildDOB { get; set; }
        public int BirthWeight { get; set; }
        public string ChildPhoto { get; set; }
        public int AccountId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<ChildSkillRequest> ChildSkillRequest { get; set; }
        public List<ChildMemoryRequest> ChildMemoryRequest { get; set; }

    }
}
