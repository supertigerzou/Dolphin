using Future.Core.Domain.Directory;
using System.Data.Entity.ModelConfiguration;

namespace Future.Data.Mapping.Directory
{
    public class CountryRegionMap : EntityTypeConfiguration<CountryRegion>
    {
        public CountryRegionMap()
        {
            this.ToTable("CountryRegion"); //, "Person");
            this.HasKey(cr => cr.Code);
            this.Property(cr => cr.Name).IsRequired().HasMaxLength(50);
            this.Property(cr => cr.ModifiedDate).IsRequired();
        }
    }
}
