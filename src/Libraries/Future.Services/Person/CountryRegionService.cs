using Future.Core.Data;
using Future.Core.Domain.Person;
using System.Collections.Generic;
using System.Linq;

namespace Future.Services.Person
{
    public class CountryRegionService : ICountryRegionService
    {
        private readonly IRepository<CountryRegion> _countryRegionRepository;

        public CountryRegionService(IRepository<CountryRegion> countryRegionRepository)
        {
            this._countryRegionRepository = countryRegionRepository;
        }

        public CountryRegion GetByCode(string code)
        {
            return _countryRegionRepository.GetByPrimaryKey(code);
        }

        public IList<CountryRegion> GetAll()
        {
            return _countryRegionRepository.Table.ToList();
        }
    }
}