using Future.Core.Domain.Person;
using System.Collections.Generic;

namespace Future.Services.Person
{
    public interface ICountryRegionService
    {
        CountryRegion GetByCode(string code);
        IList<CountryRegion> GetAll();
    }
}