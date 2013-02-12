using Future.Core.Domain.Sales;
using System.Data.Entity.ModelConfiguration;

namespace Future.Data.Mapping.Sales
{
    public class TerritoryMap : EntityTypeConfiguration<Territory>
    {
        public TerritoryMap()
        {
            this.ToTable("SalesTerritory", "Sales");
            this.HasKey(t => t.Id);
            this.Property(t => t.Id).HasColumnName("TerritoryID");
            this.Property(t => t.Name).IsRequired().HasMaxLength(50);
            this.Property(t => t.Group).HasColumnName("Group");
            this.Property(t => t.SalesYTD).HasColumnName("SalesYTD");
            this.Property(t => t.SalesLastYear).HasColumnName("SalesLastYear");
            this.Property(t => t.CostYTD).HasColumnName("CostYTD");
            this.Property(t => t.CostLastYear).HasColumnName("CostLastYear");
            this.Property(t => t.RowGuid).HasColumnName("rowguid");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            this.HasRequired(t => t.CountryRegion)
                .WithMany(cr => cr.Territories)
                .HasForeignKey(t => t.CountryRegionCode);
        }
    }
}
