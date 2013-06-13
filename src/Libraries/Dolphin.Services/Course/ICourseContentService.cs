using System.Collections.Generic;
using Dolphin.Core.Domain.Course;

namespace Dolphin.Services.Course
{
    public interface ICourseContentService
    {
        IList<CourseUnit> GetAllUnits();
    }
}