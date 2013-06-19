using System.Collections.Generic;
using Dolphin.Core.Domain.Course;

namespace Dolphin.Services.Search
{
    public interface ISearchService
    {
        void AddUpdateIndex(IEnumerable<CourseUnit> units);
    }
}