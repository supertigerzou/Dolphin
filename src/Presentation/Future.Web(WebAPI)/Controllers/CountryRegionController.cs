using Future.Core.Domain.Person;
using Future.Data;
using Future.Services.Person;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using System.Linq;
using Future.Web.ViewModel.Person;

namespace Future.Web_WebAPI_.Controllers
{
    public class CountryRegionController : ApiController
    {
        private readonly ICountryRegionService _countryRegionService;

        public CountryRegionController()
            : this(new CountryRegionService(
                new EntityFrameworkRepository<CountryRegion>(
                new FutureObjectContext(ConfigurationManager.ConnectionStrings["futureDb"].ConnectionString))))
        {

        }

        public CountryRegionController(ICountryRegionService countryRegionService)
        {
            this._countryRegionService = countryRegionService;
        }

        // GET api/<controller>
        public IEnumerable<CountryRegionModel> Get()
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