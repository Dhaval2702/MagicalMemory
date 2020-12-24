namespace WebApi.Models.Country
{
    /// <summary>
    /// This class Represents the State details Model class
    /// </summary>
    public class StateResponse
    {
        public int id { get; set; }
        public string stateName { get; set; }
        public int countryId { get; set; }
    }
}
