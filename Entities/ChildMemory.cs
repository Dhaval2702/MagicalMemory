using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class ChildMemory
    {
        [Key]
        public Guid MemoryId { get; set; }
        public string MemoryName { get; set; }
        public string MemoryPhoto { get; set; }

        [ForeignKey("ChildId")]
        public Guid ChildId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }

     //   public virtual ICollection<Children> Children { get; set; }
    }
}
