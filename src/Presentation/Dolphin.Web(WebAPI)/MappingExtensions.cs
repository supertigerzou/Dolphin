using AutoMapper;
using Dolphin.Core.Domain.Course;
using Dolphin.Core.Domain.Person;
using Dolphin.Core.Domain.Sales;
using Dolphin.Web.ViewModel.Course;
using Dolphin.Web.ViewModel.Person;
using Dolphin.Web.ViewModel.Sales;

namespace Dolphin.Web_WebAPI_
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