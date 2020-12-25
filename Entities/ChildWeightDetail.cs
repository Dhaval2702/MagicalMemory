using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities
{
    public class ChildWeightDetail
    {
        [Key]
        public Guid ChildWeightId { get; set; }
        public int ChildWeight { get; set; }
        public DateTime? WeightDate { get; set; }
        public Guid ChildId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
