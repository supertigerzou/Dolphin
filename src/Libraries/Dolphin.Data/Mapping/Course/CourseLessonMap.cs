using Dolphin.Core.Domain.Course;
using System.Data.Entity.ModelConfiguration;

namespace Dolphin.Data.Mapping.Course
{
    public class CourseLessonMap : EntityTypeConfiguration<CourseLesson>
    {
        public CourseLessonMap()
        {
            this.ToTable("CourseLesson");
            this.HasKey(cu => cu.Id);
            this.Property(cu => cu.Id).HasColumnName("CourseLesson_id");
            this.Property(cu => cu.No).HasColumnName("LessonNo");
            this.Property(cu => cu.Name).HasColumnName("LessonTopic");
            this.Property(cu => cu.Description).HasColumnName("LessonDescr");
            this.Property(cu => cu.ImageUrl).HasColumnName("LessonImageUrl");

            this.HasRequired(l => l.Unit)
                .WithMany(u => u.Lessons)
                .HasForeignKey(l => l.CourseUnit_id);
        }
    }
}