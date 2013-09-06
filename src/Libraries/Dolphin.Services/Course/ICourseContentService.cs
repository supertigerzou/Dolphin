using Dolphin.Core.Domain.Course;
using System.Collections.Generic;

namespace Dolphin.Services.Course
{
    public interface ICourseContentService
    {
        IList<CourseUnit> GetAllUnits();
        IList<ImageResource> GetAllMedias();
        TextResource GetText(int textId);
        ImageResource GetMedia(int mediaId);
    }
}