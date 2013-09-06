using Dolphin.Core.Domain.Course;
using System.Data.Entity.ModelConfiguration;

namespace Dolphin.Data.Mapping.Course
{
    public class TextResourceMap : EntityTypeConfiguration<TextResource>
    {
        public TextResourceMap()
        {
            this.ToTable("TextResource");
            this.HasKey(ir => ir.Id);
            this.Property(ir => ir.Id).HasColumnName("TextResource_Id");
            this.Property(ir => ir.Text).HasColumnName("DefaultText");
        }
    }
}