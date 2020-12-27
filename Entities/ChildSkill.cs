using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class ChildSkill
    {
        [Key]
        public Guid ChildSkillId { get; set; }
        public string SkillName { get; set; }
        public DateTime? SkillCreatedDate { get; set; }
        public DateTime? SkillUpdatedDate { get; set; }

        [ForeignKey("ChildId")]
        public Guid ChildId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

      //  public virtual ICollection<Children> Children { get; set; }
    }
}
