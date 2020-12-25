using System;

namespace WebApi.Models.Children
{
    public class ChildMemoryRequest
    {
        public string MemoryName { get; set; }
        public string MemoryPhoto { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
