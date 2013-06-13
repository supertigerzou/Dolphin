using System.Collections.Generic;
using Future.Core.Domain.Course;

namespace Future.Services.Course
{
    public interface ICourseContentService
    {
        IList<CourseUnit> GetAllUnits();
    }
}