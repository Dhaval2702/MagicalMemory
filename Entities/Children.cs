﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Children
    {
        [Key]
        public Guid ChildId { get; set; }
        public string ChildName { get; set; }
        public DateTime ChildDOB { get; set; }
        public string BirthWeight { get; set; }
        public string ChildPhoto { get; set; }
        public int AccountId { get; set; }
        public string BloodGroup { get; set; }
        public string RelationShip { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("ChildId")]
        public virtual ICollection<ChildSkill> ChildSkill { get; set; }

        [ForeignKey("ChildId")]
        public virtual ICollection<ChildMemory> ChildMemory { get; set; }

        [ForeignKey("ChildId")]
        public virtual ICollection<ChildPaymentHistory> ChildPaymentHistory { get; set; }


    }
}
