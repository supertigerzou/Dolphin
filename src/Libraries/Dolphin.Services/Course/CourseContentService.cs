using Dolphin.Core.Data;
using Dolphin.Core.Domain.Course;
using Dolphin.Services.Helpers;
using EFSchools.Englishtown.Web;
using System.Collections.Generic;
using System.Linq;

namespace Dolphin.Services.Course
{
    public class CourseContentService : ICourseContentService
    {
        private readonly IRepository<CourseUnit> _courseUnitRepository;
        private readonly Translator _translator;

        public CourseContentService(IRepository<CourseUnit> courseUnitRepository, Translator translator)
        {
            this._courseUnitRepository = courseUnitRepository;
            this._translator = translator;
        }

        public IList<CourseUnit> GetAllUnits()
        {
            var units = _courseUnitRepository.Table.Where(unit =>
                unit.Name != null && unit.Name.StartsWith(ServiceConstant.BlurbPrefix) &&
                unit.Description != null && unit.Description.StartsWith(ServiceConstant.BlurbPrefix) &&
                unit.ImageUrl != null && unit.ImageUrl.StartsWith(ServiceConstant.MediaPrefix)
            ).ToList();

            foreach (var unit in units)
            {
                unit.Name = ResourceHelper.GetTrans(_translator, unit.Name);
                unit.Description = ResourceHelper.GetTrans(_translator, unit.Description);
                unit.ImageUrl = ResourceHelper.GetMedia(_translator, unit.ImageUrl);
            }

            return units;
        }
    }
}