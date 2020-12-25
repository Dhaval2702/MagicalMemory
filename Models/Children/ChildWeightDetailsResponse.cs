using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Children
{
    public class ChildWeightDetailsResponse
    {
        public Guid ChildWeightId { get; set; }
        public int ChildWeight { get; set; }
        public DateTime? WeightDate { get; set; }
        public Guid ChildId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
