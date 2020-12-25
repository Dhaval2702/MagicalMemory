using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Children
{
    public class DependentChildrenRequest
    {

        public string ChildName { get; set; }

        public DateTime? ChildDOB { get; set; }

        public List<ChildWeightDetailRequest> ChildWeightDetail { get; set; }
        public List<ChildPhotoMemoryRequest> ChildPhotoMemory { get; set; }

    }
}
