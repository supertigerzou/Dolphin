using Dolphin.Core.Domain.Course;
using System.Data.Entity.ModelConfiguration;

namespace Dolphin.Data.Mapping.Course
{
    public class ImageResourceMap : EntityTypeConfiguration<ImageResource>
    {
        public ImageResourceMap()
        {
            this.ToTable("ImageResource");
            this.HasKey(ir => ir.Id);
            this.Property(ir => ir.Id).HasColumnName("ImageResource_Id");
            this.Property(ir => ir.Url).HasColumnName("DefaultUrl");
        }
    }
}