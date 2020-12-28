using System;

namespace WebApi.Models.Children
{
    public class ChildrenPaymentHistoryRequest
    {
        public int AccountId { get; set; }
        public Guid ChildId { get; set; }
        public string PaymentYear { get; set; }
        public bool? IsUserPaid { get; set; }
        public bool? IsUserOnTrial { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
