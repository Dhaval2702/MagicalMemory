using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Models.Country;
using WebApi.Services;

namespace WebApi.Controllers
{
    /// <summary>
    /// This controller is used to populate the Country/State data for the dropdown bindings
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class CountryController : BaseController
    {
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;

        public CountryController(
           ICountryService countryService,
           IMapper mapper)
        {
            _countryService = countryService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get All list of Contiries for Drop down Population
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all-countires")]
        public ActionResult<CountryResponse> GetAllContires()
        {
            var response = _countryService.GetListofCountries();
            return Ok(response);
        }

        /// <summary>
        /// Get state by Country Id.
        /// </summary>
        /// <param name="countryId">country Id</param>
        /// <returns></returns>
        [HttpGet("get-state-by-countires")]
        public ActionResult<IEnumerable<StateResponse>> GetStateByCountry(int countryId)
        {
            var response = _countryService.GetStateByCountry(countryId);
            return Ok(response);
        }

        [HttpGet("get-countries-by-name")]
        public ActionResult<IEnumerable<CountryResponse>> SearchCountriesByName(string countryName)
        {
            var response = _countryService.SearchCountriesByName(countryName);
            return Ok(response);
        }
    }
}
