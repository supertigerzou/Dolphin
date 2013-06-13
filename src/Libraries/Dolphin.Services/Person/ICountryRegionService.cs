using Dolphin.Core.Domain.Person;
using System.Collections.Generic;

namespace Dolphin.Services.Person
{
    public interface ICountryRegionService
    {
        CountryRegion GetByCode(string code);
        IList<CountryRegion> GetAll();
    }
}