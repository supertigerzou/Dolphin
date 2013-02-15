using Future.Core.Domain.Person;
using Future.Core.Domain.Sales;
using NUnit.Framework;
using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
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
                Name = "Northwest",
                Group = "North America",
                SalesYTD = (decimal)7887186.78,
                SalesLastYear = (decimal)3298694.49,
                CostYTD = (decimal)0.00,
                CostLastYear = (decimal)0.00,
                RowGuid = Guid.Parse("43689A10-E30B-497F-B0DE-11DE20267FF7"),
                ModifiedDate = DateTime.Now
            };
            Save(territory);

            var countryRegion = Context.Set<CountryRegion>().Find("US");
            Assert.IsNotNull(countryRegion);
            Assert.IsTrue(countryRegion.Territories.Count == 1);

            countryRegion = new CountryRegion { Code = "CN", Name = "China", ModifiedDate = DateTime.Now };
            countryRegion.Territories.Add(new Territory
            {
                Name = "Southwest",
                Group = "North America",
                SalesYTD = (decimal)7887186.78,
                SalesLastYear = (decimal)3298694.49,
                CostYTD = (decimal)0.00,
                CostLastYear = (decimal)0.00,
                RowGuid = Guid.Parse("43689A10-E30B-497F-B0DE-11DE20267FF7"),
                ModifiedDate = DateTime.Now
            });
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

            territory = new Territory
            {
                Name = "Northwest",
                Group = "North America",
                SalesYTD = (decimal)7887186.78,
                SalesLastYear = (decimal)3298694.49,
                CostYTD = (decimal)0.00,
                CostLastYear = (decimal)0.00,
                RowGuid = Guid.Parse("43689A10-E30B-497F-B0DE-11DE20267FF7"),
                ModifiedDate = DateTime.Now,
                CountryRegion = Context.Set<CountryRegion>().Single(cr => cr.Code == "CN")
            };
            Save(territory, lazyLoad: false);

            countryRegion = Context.Set<CountryRegion>().Find("CN");
            Assert.IsNotNull(countryRegion);
            Assert.IsTrue(countryRegion.Territories.Count == 0);
            Context.Entry(countryRegion).Collection("Territories").Load();
            Assert.IsTrue(countryRegion.Territories.Count == 2);

            DisposeAndRecreateContext(false);

            countryRegion = Context.Set<CountryRegion>().Find("CN"); // China
            Context.Entry(countryRegion).Collection("Territories").Query().Cast<Territory>().Where(t => t.Name == "Northwest")
                .Load();
            Assert.IsTrue(countryRegion.Territories.Count == 1);

            DisposeAndRecreateContext(false);

            countryRegion = Context.Set<CountryRegion>().Find("CN"); // China
            Assert.IsTrue(Context.Entry(countryRegion).Collection("Territories").Query().Cast<Territory>().Count() == 2);

            DisposeAndRecreateContext();
            // Verifying the table query with LazyLoad enabled
            countryRegion = Context.Set<CountryRegion>().ToList()[1];
            Assert.IsTrue(countryRegion.Territories.Count == 2);

            // Below link is really helpful for Entity Framework lazy/eager loading.
            // Reference: http://msdn.microsoft.com/en-US/data/jj574232
        }

        private bool IsProxy(object type)
        {
            return type != null && ObjectContext.GetObjectType(type.GetType()) != type.GetType();
        }

        [Test]
        public void TestIfEntityIsProxy()
        {
            using (var context = ((IObjectContextAdapter)Context).ObjectContext)
            {
                var countryRegion = context.CreateObject<CountryRegion>();
                countryRegion.Code = "CA";
                countryRegion.Name = "Canada";
                Context.Set<CountryRegion>().Attach(countryRegion);

                // Determine if the instance is a proxy.
                // If it is a proxy it supports lazy loading.
                var isLazyLoading = IsProxy(countryRegion);
                Assert.IsTrue(isLazyLoading);

                // Determine if it is a change tracking proxy by
                // making a change and verifying that it was detected.
                countryRegion.Name = "China";
                var isChangeTracking = context.ObjectStateManager
                                          .GetObjectStateEntry(countryRegion)
                                          .State == EntityState.Modified;
                Assert.IsTrue(isChangeTracking);
            }
        }
    }
}
