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
            var imageResourceRepo = new EntityFrameworkRepository<ImageResource>(new ResourceObjectContext("resourceDb"));
            container.RegisterInstance(typeof(IRepository<CourseUnit>), courseUnitRepo);
            container.RegisterInstance(typeof(IRepository<CourseLesson>), courseLessonRepo);
            container.RegisterInstance(typeof(IRepository<ImageResource>), imageResourceRepo);
            container.RegisterType<ISearchService, SearchService>();
            container.RegisterType<ICourseContentService, CourseContentService>();
        }
    }
}