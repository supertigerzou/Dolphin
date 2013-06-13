using EFSchools.Englishtown.Resources;
using Dolphin.Core.Data;
using Dolphin.Core.Domain.Course;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Dolphin.Services.Course
{
    public class CourseContentService : ICourseContentService
    {
        private readonly IRepository<CourseUnit> _courseUnitRepository;
        private readonly ITranslator _translator;

        public CourseContentService(IRepository<CourseUnit> courseUnitRepository, ITranslator translator)
        {
            this._courseUnitRepository = courseUnitRepository;
            this._translator = translator;
        }

        public IList<CourseUnit> GetAllUnits()
        {
            var units = _courseUnitRepository.Table.ToList();
            foreach (var unit in units)
            {
                if (unit.Name != null && unit.Name.Contains("getTrans::"))
                {
                    var blurbId = int.Parse(unit.Name.Substring("getTrans::".Length));
                    unit.Name = this._translator.GetTrans(blurbId, CultureInfo.CreateSpecificCulture("en"));
                }
            }

            return units;
        }
    }
}