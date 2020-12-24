using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using WebApi.Helpers;
using WebApi.Models.Country;

namespace WebApi.Services
{
    /// <summary>
    /// Interface for Country Service
    /// </summary>
    public interface ICountryService
    {
        IEnumerable<CountryResponse> GetListofCountries();
        IEnumerable<StateResponse> GetStateByCountry(int countryId);

        IEnumerable<CountryResponse> SearchCountriesByName(string countryName);

        /// <summary>
        /// Country Service.
        /// </summary>
        public class CountryService : ICountryService
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            /// <summary>
            /// Constructor to intialize the data contenxt for country service.
            /// </summary>
            /// <param name="context"></param>
            /// <param name="mapper"></param>
            public CountryService(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;

            }

            /// <summary>
            /// Get List of Countries.
            /// </summary>
            /// <returns>List of countires</returns>
            public IEnumerable<CountryResponse> GetListofCountries()
            {
                var countries = _context.countries;
                return _mapper.Map<IList<CountryResponse>>(countries);
            }

            /// <summary>
            /// Get State Details from Country filter by Country ID
            /// </summary>
            /// <param name="countryId"></param>
            /// <returns></returns>
            public IEnumerable<StateResponse> GetStateByCountry(int countryId)
            {
                var countries = _context.states.Where(y => y.countryId == countryId);
                return _mapper.Map<IEnumerable<StateResponse>>(countries);
            }

            public IEnumerable<CountryResponse> SearchCountriesByName(string countryName)
            {
                var countries = _context.countries.Where(x=>x.countryName.Contains(countryName));
                return _mapper.Map<IList<CountryResponse>>(countries);
            }







        }

    }
}
