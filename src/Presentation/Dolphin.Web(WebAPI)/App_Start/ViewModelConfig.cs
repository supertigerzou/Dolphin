using AutoMapper;
using Dolphin.Core.Domain.Course;
using Dolphin.Core.Domain.Person;
using Dolphin.Core.Domain.Sales;
using Dolphin.Web.ViewModel.Course;
using Dolphin.Web.ViewModel.Person;
using Dolphin.Web.ViewModel.Sales;
using System.Linq;

namespace Dolphin.Web.WebAPI.App_Start
{
    public class ViewModelConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<Territory, TerritoryViewModel>();
            Mapper.CreateMap<CountryRegion, CountryRegionViewModel>()
                .ForMember(crm => crm.Territories, mo => mo.MapFrom(cr => cr.Territories.Select(t => t.ToModel()).ToArray()));
            Mapper.CreateMap<CourseUnit, CourseUnitViewModel>()
                .ForMember(cum => cum.Lessons, mo => mo.MapFrom(cu => cu.Lessons.Select(l => l.ToModel())
                    .Where(lesson => lesson.ImageUrl != null).ToArray()));
            Mapper.CreateMap<CourseLesson, CourseLessonViewModel>();
        }
    }
}