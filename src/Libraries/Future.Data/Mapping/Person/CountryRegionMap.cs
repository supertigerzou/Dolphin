﻿using Future.Core.Domain.Person;
using System.Data.Entity.ModelConfiguration;

namespace Future.Data.Mapping.Person
{
    public class CountryRegionMap : EntityTypeConfiguration<CountryRegion>
    {
        public CountryRegionMap()
        {
            this.ToTable("CountryRegion"); //, "Person");
            this.HasKey(cr => cr.Code);
            this.Property(cr => cr.Code).IsRequired().HasMaxLength(3);
            this.Property(cr => cr.Name).IsRequired().HasMaxLength(50);
            this.Property(cr => cr.ModifiedDate).IsRequired();
        }
    }
}
