using Future.Tests;

namespace Future.Data.Tests
{
    public class EntityFrameworkPersistenceTestBase : PersistenceTestBase
    {
        protected override string ConnectionString
        {
            get
            {
                return "Server=.\\SQLExpress;Integrated Security=true;Database=Future.Data.Tests";
            }
        }
    }
}
