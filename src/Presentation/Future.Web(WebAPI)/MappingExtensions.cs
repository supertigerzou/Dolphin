using AutoMapper;
using Future.Core.Domain.Person;
using Future.Core.Domain.Sales;
using Future.Web.ViewModel.Person;
using Future.Web.ViewModel.Sales;

namespace Future.Web_WebAPI_
{
    public static class MappingExtensions
    {
        public static CountryRegionModel ToModel(this CountryRegion record)
        {
            return Mapper.Map<CountryRegion, CountryRegionModel>(record);
        }

        public static TerritoryModel ToModel(this Territory record)
        {
            return Mapper.Map<Territory, TerritoryModel>(record);
        }
    }
}