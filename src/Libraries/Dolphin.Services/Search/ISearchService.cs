using Dolphin.Core.Domain.Course;
using System.Collections.Generic;

namespace Dolphin.Services.Search
{
    public interface ISearchService
    {
        void AddUpdateIndex(IEnumerable<CourseUnit> units);
        IEnumerable<CourseUnit> Search(string input, string fieldName = "");
    }
}