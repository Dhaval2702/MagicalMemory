namespace WebApi.Models.Payment
{
    public class PayUPaymentSetting
    {
        public string MerchantKey { get; set; }
        public string Salt { get; set; }
        public string PayuBaseUrl { get; set; }
        public string hashSequence { get; set; }
        public string SuccessUrl { get; set; }
        public string FailureUrl { get; set; }

        public string ProductInfo { get; set; }
        public string Amount { get; set; }

    }
}
