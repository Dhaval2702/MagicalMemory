using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.Children
{
    public class ChildrenPaymentHistoryResponse
    {
        public Guid ChildPaymentId { get; set; }
        [ForeignKey("AccountId")]
        public int AccountId { get; set; }
        [ForeignKey("ChildId")]
        public Guid ChildId { get; set; }
        public string PaymentYear { get; set; }
        public bool IsUserPaid { get; set; }
        public bool IsUserOnTrial { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
