using Dolphin.Core.Domain.Course;
using System.Collections.Generic;

namespace Dolphin.Services.Course
{
    public interface ICourseContentService
    {
        IList<CourseUnit> GetAllUnits();
        IList<ImageResource> GetAllMedias();
        ImageResource GetMedia(int mediaId);
    }
}