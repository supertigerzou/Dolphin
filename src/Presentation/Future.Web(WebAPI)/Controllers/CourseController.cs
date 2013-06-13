using EFSchools.Englishtown.Resources;
using Future.Core.Domain.Course;
using Future.Data;
using Future.Services.Course;
using Future.Web.ViewModel.Course;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;

namespace Future.Web_WebAPI_.Controllers
{
    public class CourseController : ApiController
    {
        private readonly ICourseContentService _courseContentService;

        public CourseController()
            : this(new CourseContentService(
                new EntityFrameworkRepository<CourseUnit>(
                new CourseObjectContext((ConfigurationManager.ConnectionStrings["courseDb"].ConnectionString))),
                Global.Container.Resolve<ITranslator>()))
        {

        }

        public CourseController(ICourseContentService courseContentService)
        {
            this._courseContentService = courseContentService;
        }

        // GET api/<controller>
        public IEnumerable<CourseUnitViewModel> Get()
        {
            return _courseContentService.GetAllUnits().Select(cu => cu.ToModel());
        }
    }
}