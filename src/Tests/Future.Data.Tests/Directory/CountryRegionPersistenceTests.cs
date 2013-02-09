using Future.Core.Domain.Directory;
using NUnit.Framework;
using System;

namespace Future.Data.Tests.Directory
{
    [TestFixture]
    public class CountryRegionPersistenceTests
    {
        [Test]
        public void Can_save_and_load_country_region()
        {
            var countryRegion = new CountryRegion { Code = "AD", Name = "Andorra", ModifiedDate = DateTime.Now };

            IDbContext context = new FutureObjectContext();
            context.Database.Delete();
            context.Database.Create();

            context.Set<CountryRegion>().Add(countryRegion);
            context.SaveChanges();
        }
    }
}
