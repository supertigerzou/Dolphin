using Dolphin.Core.Data;
using Dolphin.Core.Domain.Course;
using Dolphin.Data;
using Dolphin.Services.Course;
using Dolphin.Services.Search;
using Microsoft.Practices.Unity;

namespace Dolphin.Web.WebAPI.App_Start
{
    public static class UnityConfig
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterInstance(typeof(IUnityContainer), container);

            var courseUnitRepo = new EntityFrameworkRepository<CourseUnit>(new CourseObjectContext("courseDb"));
            var courseLessonRepo = new EntityFrameworkRepository<CourseLesson>(new CourseObjectContext("courseDb"));
            container.RegisterInstance(typeof(IRepository<CourseUnit>), courseUnitRepo);
            container.RegisterInstance(typeof(IRepository<CourseLesson>), courseLessonRepo);
            container.RegisterType<ISearchService, SearchService>();
            container.RegisterType<ICourseContentService, CourseContentService>();
        }
    }
}