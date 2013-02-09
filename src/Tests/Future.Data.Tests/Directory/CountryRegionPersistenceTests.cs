using Future.Core.Domain.Directory;
using NUnit.Framework;
using System;
using System.Data.Entity;
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

            Database.DefaultConnectionFactory = new LocalDbConnectionFactory("v11.0");
            IDbContext context = new FutureObjectContext(ConnectionString);
            context.Database.Delete();
            context.Database.Create();

            context.Set<CountryRegion>().Add(countryRegion);
            context.SaveChanges();
        }
    }
}
