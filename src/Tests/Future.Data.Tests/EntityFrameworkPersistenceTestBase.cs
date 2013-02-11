using Future.Tests;
using System;
using System.Data.Entity.Infrastructure;

namespace Future.Data.Tests
{
    public class EntityFrameworkPersistenceTestBase : PersistenceTestBase
    {
        protected IDbContext Context;

        public override void TestFixtureSetUp()
        {
            Context = new FutureObjectContext(ConnectionString);
            Context.Database.Delete();
            Context.Database.Create();

            base.TestFixtureSetUp();
        }

        protected void Save<T>(T record, bool disposeContext = true, bool lazyLoad = true) where T : class
        {
            Context.Set<T>().Add(record);
            Console.WriteLine(((IObjectContextAdapter)Context).ObjectContext.ToTraceString());
            Context.SaveChanges();

            if (disposeContext)
            {
                DisposeAndRecreateContext(lazyLoad);
            }
        }

        protected void DisposeAndRecreateContext(bool lazyLoad = true)
        {
            ((IDisposable)Context).Dispose();
            Context = new FutureObjectContext(ConnectionString, lazyLoad);
        }

        protected override string ConnectionString
        {
            get
            {
                return "Server=.\\SQLExpress;Integrated Security=true;Database=Future_Data_Tests";
            }
        }
    }
}
