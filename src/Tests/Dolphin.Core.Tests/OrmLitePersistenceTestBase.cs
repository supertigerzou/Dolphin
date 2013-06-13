using Dolphin.Tests;

namespace Dolphin.Core.Tests
{
    public class OrmLitePersistenceTestBase : PersistenceTestBase
    {
        protected override string ConnectionString
        {
            get
            {
                return "Server=.\\SQLExpress;Integrated Security=true;Database=Dolphin.Core.Tests";
            }
        }
    }
}
