using Dolphin.Core.Domain.Person;
using Dolphin.Data;
using Dolphin.Services.Person;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using System.Linq;
using Dolphin.Web.ViewModel.Person;

namespace Dolphin.Web_WebAPI_.Controllers
{
    public class CountryRegionController : ApiController
    {
        private readonly ICountryRegionService _countryRegionService;

        public CountryRegionController()
            : this(new CountryRegionService(
                new EntityFrameworkRepository<CountryRegion>(
                new DolphinObjectContext(ConfigurationManager.ConnectionStrings["futureDb"].ConnectionString))))
        {

        }

        public CountryRegionController(ICountryRegionService countryRegionService)
        {
            this._countryRegionService = countryRegionService;
        }

        // GET api/<controller>
        public IEnumerable<CountryRegionViewModel> Get()
        {
            return _countryRegionService.GetAll().Select(cr => cr.ToModel());
        }

        // GET api/<controller>/5
        public CountryRegion Get(string code)
        {
            var countryRegion = _countryRegionService.GetByCode(code);

            return countryRegion;
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}