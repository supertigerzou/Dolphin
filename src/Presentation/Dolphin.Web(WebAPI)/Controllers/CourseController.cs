using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using Dolphin.Core.Domain.Course;
using Dolphin.Data;
using Dolphin.Services.Course;
using Dolphin.Web.ViewModel.Course;
using Dolphin.Web.ViewModel.Search;
using Dolphin.Web_WebAPI_;
using EFSchools.Englishtown.Resources;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;

namespace Dolphin.Web.WebAPI.Controllers
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

        [System.Web.Http.HttpPost]
        public IEnumerable<CourseUnitViewModel> Search(SearchParams searchParams)
        {
            return _courseContentService.GetAllUnits().Take(100).Select(cu => cu.ToModel());
        }
    }
}