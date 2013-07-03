using ServiceStack.DataAnnotations;

namespace Dolphin.Core.Domain.Course
{
    public class CourseLesson
    {
        [Alias("CourseLesson_id")]
        public virtual int Id { get; set; }

        [Alias("LessonNo")]
        public virtual byte No { get; set; }

        [Alias("LessonTopic")]
        public virtual string Name { get; set; }

        [Alias("LessonDescr")]
        public virtual string Description { get; set; }

        [Alias("LessonImageUrl")]
        public virtual string ImageUrl { get; set; }

        public int CourseUnit_id { get; set; }

        public virtual CourseUnit Unit { get; set; }
    }
}