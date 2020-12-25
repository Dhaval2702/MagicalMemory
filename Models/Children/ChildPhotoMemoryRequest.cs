using System;

namespace WebApi.Models.Children
{
    public class ChildPhotoMemoryRequest
    {
        public string ChildPhotomemoryName { get; set; }
        public string ChilePhotoPath { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
