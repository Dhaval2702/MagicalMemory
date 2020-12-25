using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities
{
    public class DependentChildren
    {
        [Key]
        public Guid ChildId { get; set; }
        public string ChildName { get; set; }
        public DateTime? ChildDOB { get; set; }
        public Guid? ChildSkillId { get; set; }
        public Guid? ChildWeightId { get; set; }
        public Guid? ChildPhotoMemoryId { get; set; }
        public int AccountId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public List<ChildWeightDetail> ChildWeightDetail { get; set; }
        public List<ChildPhotoMemory> ChildPhotoMemory { get; set; }



    }
}
