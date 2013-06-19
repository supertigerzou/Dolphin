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
            var units = _courseUnitRepository.Table.Where(unit => unit.Name != null).ToList();
            foreach (var unit in units)
            {
                if (unit.Name.Contains("getTrans::"))
                {
                    var blurbId = int.Parse(unit.Name.Substring("getTrans::".Length));
                    unit.Name = this._translator.GetTrans(blurbId, CultureInfo.CreateSpecificCulture("en"));
                }

                if (unit.Description != null && unit.Description.Contains("getTrans::"))
                {
                    var blurbId = int.Parse(unit.Description.Substring("getTrans::".Length));
                    unit.Description = this._translator.GetTrans(blurbId, CultureInfo.CreateSpecificCulture("en"));
                }
            }

            return units;
        }
    }
}