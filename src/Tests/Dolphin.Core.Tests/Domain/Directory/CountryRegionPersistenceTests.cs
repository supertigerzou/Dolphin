using Dolphin.Core.Domain.Person;
using Dolphin.Core.Domain.Sales;
using NUnit.Framework;
using ServiceStack.OrmLite;
using System;

namespace Dolphin.Core.Tests.Domain.Directory
{
    [TestFixture]
    public class CountryRegionPersistenceTests : OrmLitePersistenceTestBase
    {
        [Test]
        public void Can_save_and_load_country_region()
        {
            var factory = new OrmLiteConnectionFactory(ConnectionString, SqlServerDialect.Provider);

            using (var db = factory.OpenDbConnection())
            {
                db.DropTables(typeof(Territory), typeof(CountryRegion));
                db.CreateTables(false, typeof(CountryRegion), typeof(Territory));

                db.Insert(new CountryRegion { Code = "AD", Name = "Andorra", ModifiedDate = DateTime.Now });

                using (var dbTrans = db.BeginTransaction())
                {
                    db.Insert(new CountryRegion { Code = "AE", Name = "United Arab Emirates", ModifiedDate = DateTime.Now });
                    db.Insert(new CountryRegion { Code = "AF", Name = "Afghanistan", ModifiedDate = DateTime.Now });

                    dbTrans.Commit();
                }

                Assert.That(db.Select<CountryRegion>(), Has.Count.EqualTo(3));

                using (var dbTrans = db.BeginTransaction())
                {
                    db.Insert(new CountryRegion { Code = "AG", Name = "Antigua and Barbuda", ModifiedDate = DateTime.Now });
                    Assert.That(db.Select<CountryRegion>(), Has.Count.EqualTo(4));

                    dbTrans.Rollback();
                }

                Assert.That(db.Select<CountryRegion>(), Has.Count.EqualTo(3));

                db.Insert(new CountryRegion { Code = "US", Name = "United States", ModifiedDate = DateTime.Now });
                db.Insert(new Territory
                              {
                                  CountryRegionCode = "US",
                                  Name = "Northwest"
                              });
                db.Insert(new Territory
                              {
                                  CountryRegionCode = "US",
                                  Name = "Northeast"
                              });

                var usRegion = db.Select<CountryRegion>(cr => cr.Code == "US")[0];
                var territories = db.Select<Territory>(t => t.CountryRegionCode == usRegion.Code);
                territories.ForEach(t => t.CountryRegion = usRegion);
                usRegion.Territories = territories;

                Assert.IsTrue(usRegion.Territories.Count == 2);
            }
        }
    }
}
