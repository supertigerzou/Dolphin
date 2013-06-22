using Dolphin.Core.Data;
using Dolphin.Core.Domain.Course;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EFSchools.Englishtown.Web;

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
            var units = _courseUnitRepository.Table.Where(unit => unit.Name != null).ToList();
            foreach (var unit in units)
            {
                if (unit.Name.Contains("getTrans::"))
                {
                    var blurbId = int.Parse(unit.Name.Substring("getTrans::".Length));
                    unit.Name = this._translator.GetTrans(blurbId, "en");
                }

                if (unit.Description != null && unit.Description.Contains("getTrans::"))
                {
                    var blurbId = int.Parse(unit.Description.Substring("getTrans::".Length));
                    unit.Description = this._translator.GetTrans(blurbId, "en");
                }

                if (unit.ImageUrl != null && unit.ImageUrl.Contains("getMedia::"))
                {
                    var mediaId = int.Parse(unit.ImageUrl.Substring("getMedia::".Length));
                    unit.ImageUrl = this._translator.GetMedia(mediaId, "en");
                }
                else
                {
                    unit.ImageUrl = string.Empty;
                }
            }

            return units;
        }
    }
}