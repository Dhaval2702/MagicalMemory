namespace WebApi.Models.Payment
{
    public class PayUPaymentRequest
    {
        public string TransectionId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProductInfo { get; set; }

        public string Amount { get; set; }

        public string PhoneNumber { get; set; }
        public string cancelURI { get; set; }
        public string AdrressLine1 { get; set; }
        public string AdrressLine2 { get; set; }
        public string City { get; set; }
        public string  Country { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string ServiceProvider { get; set; }
        public string MyProperty { get; set; }

        public string HashString { get; set; }


    }
}
