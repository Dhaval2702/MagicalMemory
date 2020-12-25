using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class ChildPhotoMemory
    {
        public Guid ChildPhotoMemoryId { get; set; }
        public string ChildPhotomemoryName { get; set; }
        public string ChilePhotoPath { get; set; }
        public Guid ChildId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }

    }
}
