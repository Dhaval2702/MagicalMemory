﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class ChildPaymentHistory
    {
        [Key]
        public Guid ChildPaymentId { get; set; }
        [ForeignKey("AccountId")]
        public int AccountId { get; set; }
        [ForeignKey("ChildId")]
        public string PaymentYear { get; set; }
        public Guid ChildId { get; set; }
        public bool IsUserPaid { get; set; }
        public bool IsUserOnTrial { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}