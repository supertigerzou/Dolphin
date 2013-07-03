using ServiceStack.DataAnnotations;
using System.Collections.Generic;
namespace Dolphin.Core.Domain.Course
{
    public class CourseUnit
    {
        private ICollection<CourseLesson> _lessons;

        [Alias("CourseUnit_id")]
        public virtual int Id { get; set; }

        [Alias("UnitNo")]
        public virtual byte No { get; set; }

        [Alias("UnitName")]
        public virtual string Name { get; set; }

        [Alias("UnitDescr")]
        public virtual string Description { get; set; }

        [Alias("UnitImageUrl")]
        public virtual string ImageUrl { get; set; }

        public virtual ICollection<CourseLesson> Lessons
        {
            get { return _lessons ?? (_lessons = new List<CourseLesson>()); }
            protected set { _lessons = value; }
        }
    }
}