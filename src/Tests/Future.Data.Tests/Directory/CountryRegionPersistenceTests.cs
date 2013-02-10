using Future.Core.Domain.Directory;
using NUnit.Framework;
using System;
using System.Data.Entity.Infrastructure;

namespace Future.Data.Tests.Directory
{

    [TestFixture]
    public class CountryRegionPersistenceTests : EntityFrameworkPersistenceTestBase
    {
        [Test]
        public void Can_save_and_load_country_region()
        {
            var countryRegion = new CountryRegion { Code = "AD", Name = "Andorra", ModifiedDate = DateTime.Now };

            IDbContext context = new FutureObjectContext(ConnectionString);
            context.Database.CreateIfNotExists();

            context.Database.ExecuteSqlCommand("truncate table dbo.[CountryRegion]");

            context.Set<CountryRegion>().Add(countryRegion);
            Console.WriteLine((context as IObjectContextAdapter).ObjectContext.ToTraceString());

            context.SaveChanges();
        }
    }
}
