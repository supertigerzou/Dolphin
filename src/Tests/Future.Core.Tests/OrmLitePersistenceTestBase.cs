using Future.Tests;

namespace Future.Core.Tests
{
    public class OrmLitePersistenceTestBase : PersistenceTestBase
    {
        protected override string ConnectionString
        {
            get
            {
                return "Server=.\\SQLExpress;Integrated Security=true;Database=Future.Core.Tests";
            }
        }
    }
}
