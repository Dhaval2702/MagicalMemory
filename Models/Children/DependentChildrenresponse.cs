using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Children
{
    public class DependentChildrenresponse
    {
        public Guid ChildId { get; set; }
        public string ChildName { get; set; }
        public DateTime? ChildDOB { get; set; }
        public Guid ChildSkillId { get; set; }
        public Guid ChildWeightId { get; set; }
        public Guid ChildPhotoMemoryId { get; set; }
        public int AccountId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public List<ChildWeightDetailsResponse> ChildWeightDetail { get; set; }
        public List<ChildPhotoMemoryResponse> ChildPhotoMemory { get; set; }

    }
}
