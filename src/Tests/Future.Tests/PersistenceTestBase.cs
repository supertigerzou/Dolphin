using NUnit.Framework;
using ServiceStack.Common.Utils;

namespace Future.Tests
{
    public class PersistenceTestBase
    {
        protected virtual string ConnectionString { get; set; }

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            ConnectionString = "~/App_Data/Database.mdf".MapAbsolutePath();
        }
    }
}
