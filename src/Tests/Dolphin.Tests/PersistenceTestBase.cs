using NUnit.Framework;
using ServiceStack.Common.Utils;
using ServiceStack.Logging;
using ServiceStack.Logging.Support.Logging;

namespace Dolphin.Tests
{
    public class PersistenceTestBase
    {
        protected virtual string ConnectionString { get; set; }

        [TestFixtureSetUp]
        public virtual void TestFixtureSetUp()
        {
            LogManager.LogFactory = new ConsoleLogFactory();

            ConnectionString = "~/App_Data/Database.mdf".MapAbsolutePath();
        }
    }
}
