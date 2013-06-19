using ServiceStack.DataAnnotations;
namespace Dolphin.Core.Domain.Course
{
    public class CourseUnit
    {
        [Alias("CourseUnit_id")]
        public virtual int Id { get; set; }

        [Alias("UnitNo")]
        public virtual byte No { get; set; }

        [Alias("UnitName")]
        public virtual string Name { get; set; }

        [Alias("UnitDescr")]
        public virtual string Description { get; set; }
    }
}