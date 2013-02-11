using Future.Core.Domain.Sales;
using System.Data.Entity.ModelConfiguration;

namespace Future.Data.Mapping.Sales
{
    public class TerritoryMap : EntityTypeConfiguration<Territory>
    {
        public TerritoryMap()
        {
            this.ToTable("SalesTerritory");
            this.HasKey(t => t.Id);
            this.Property(t => t.Name).IsRequired().HasMaxLength(50);

            this.HasRequired(t => t.CountryRegion)
                .WithMany(cr => cr.Territories)
                .HasForeignKey(t => t.CountryRegionCode);
        }
    }
}
