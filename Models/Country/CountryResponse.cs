using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Country
{
    public class CountryResponse
    {
        public int id { get; set; }
        public string sortName { get; set; }

        public string countryName { get; set; }

        public int countryCode { get; set; }
    }
}
