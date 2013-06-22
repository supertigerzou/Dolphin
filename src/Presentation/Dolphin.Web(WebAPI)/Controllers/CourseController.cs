using Dolphin.Core.Domain.Course;
using Dolphin.Data;
using Dolphin.Services.Course;
using Dolphin.Services.Search;
using Dolphin.Web.ViewModel.Course;
using Dolphin.Web.ViewModel.Search;
using Dolphin.Web_WebAPI_;
using EFSchools.Englishtown.Web;
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
        private readonly ISearchService _searchService;

        public CourseController()
            : this(new CourseContentService(
                new EntityFrameworkRepository<CourseUnit>(
                new CourseObjectContext((ConfigurationManager.ConnectionStrings["courseDb"].ConnectionString))),
                Global.Container.Resolve<Translator>()), 
                new SearchService())
        {

        }

        public CourseController(ICourseContentService courseContentService, ISearchService searchService)
        {
            this._courseContentService = courseContentService;
            this._searchService = searchService;
        }

        // GET api/<controller>
        public IEnumerable<CourseUnitViewModel> Get()
        {
            return _courseContentService.GetAllUnits().Select(cu => cu.ToModel());
        }

        [HttpPost]
        public IEnumerable<CourseUnitViewModel> Search(SearchParams searchParams)
        {
            return _searchService.Search(searchParams.SearchTerm).Select(cu => cu.ToModel());
        }
    }
}