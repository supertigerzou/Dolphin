using AutoMapper;
using Future.Core.Domain.Course;
using Future.Core.Domain.Person;
using Future.Core.Domain.Sales;
using Future.Web.ViewModel.Course;
using Future.Web.ViewModel.Person;
using Future.Web.ViewModel.Sales;

namespace Future.Web_WebAPI_
{
    public static class MappingExtensions
    {
        public static CountryRegionViewModel ToModel(this CountryRegion record)
        {
            return Mapper.Map<CountryRegion, CountryRegionViewModel>(record);
        }

        public static TerritoryViewModel ToModel(this Territory record)
        {
            return Mapper.Map<Territory, TerritoryViewModel>(record);
        }

        public static CourseUnitViewModel ToModel(this CourseUnit record)
        {
            return Mapper.Map<CourseUnit, CourseUnitViewModel>(record);
        }
    }
}