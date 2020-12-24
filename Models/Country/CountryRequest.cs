namespace WebApi.Models.Country
{
    public class CountryRequest
    {
        public int id { get; set; }
        public string sortName { get; set; }

        public string countryName { get; set; }

        public int countryCode { get; set; }
    }
}
