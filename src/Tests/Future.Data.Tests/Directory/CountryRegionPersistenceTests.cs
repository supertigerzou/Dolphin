﻿using Future.Core.Domain.Person;
using Future.Core.Domain.Sales;
using NUnit.Framework;
using System;
using System.Data.Entity;
using System.Linq;

namespace Future.Data.Tests.Directory
{
    [TestFixture]
    public class CountryRegionPersistenceTests : EntityFrameworkPersistenceTestBase
    {
        [Test]
        public void Can_save_and_load_country_region()
        {
            var territory = new Territory
            {
                CountryRegion = new CountryRegion { Code = "US", Name = "United States", ModifiedDate = DateTime.Now },
                Name = "Northwest"
            };
            Save(territory);

            var countryRegion = Context.Set<CountryRegion>().Find("US");
            Assert.IsNotNull(countryRegion);
            Assert.IsTrue(countryRegion.Territories.Count == 1);

            countryRegion = new CountryRegion { Code = "CN", Name = "China", ModifiedDate = DateTime.Now };
            countryRegion.Territories.Add(new Territory { Name = "Southwest" });
            Save(countryRegion);

            territory = Context.Set<Territory>().Find(2);
            Assert.IsNotNull(territory);
            Assert.IsTrue(territory.CountryRegion.Code == "CN");

            DisposeAndRecreateContext(false);

            territory = Context.Set<Territory>().Single(t => t.Name == "Southwest");
            Assert.IsNotNull(territory);
            Assert.IsNull(territory.CountryRegion);
            Context.Entry(territory).Reference("CountryRegion").Load();
            Assert.IsNotNull(territory.CountryRegion);

            DisposeAndRecreateContext(false);

            countryRegion = Context.Set<CountryRegion>().Find("US");
            Assert.IsNotNull(countryRegion);
            Assert.IsTrue(countryRegion.Territories.Count == 0);
            Context.Entry(countryRegion).Collection("Territories").Load();
            Assert.IsTrue(countryRegion.Territories.Count == 1);

            territory = new Territory { Name = "Northwest", CountryRegion = Context.Set<CountryRegion>().Single(cr => cr.Code == "CN") };
            Save(territory, lazyLoad: false);

            countryRegion = Context.Set<CountryRegion>().Find("CN");
            Assert.IsNotNull(countryRegion);
            Assert.IsTrue(countryRegion.Territories.Count == 0);
            Context.Entry(countryRegion).Collection("Territories").Load();
            Assert.IsTrue(countryRegion.Territories.Count == 2);

            DisposeAndRecreateContext(false);

            countryRegion = Context.Set<CountryRegion>().Find("CN"); // China
            Context.Entry(countryRegion).Collection("Territories").Query().Cast<Territory>().Where(t => t.Name == "Northwest").Load();
            Assert.IsTrue(countryRegion.Territories.Count == 1);

            DisposeAndRecreateContext(false);

            countryRegion = Context.Set<CountryRegion>().Find("CN"); // China
            Assert.IsTrue(Context.Entry(countryRegion).Collection("Territories").Query().Cast<Territory>().Count() == 2);

            // Below link is really helpful for Entity Framework lazy/eager loading.
            // Reference: http://msdn.microsoft.com/en-US/data/jj574232
        }
    }
}
