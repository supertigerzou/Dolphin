using Future.Core.Domain.Directory;
using NUnit.Framework;
using ServiceStack.OrmLite;
using System;

namespace Future.Core.Tests.Domain.Directory
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
                db.DropAndCreateTable(typeof(CountryRegion));

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
            }
        }
    }
}
