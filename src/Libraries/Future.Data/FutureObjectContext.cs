using Future.Data.Mapping.Directory;
using System.Data.Entity;
using System.Data.Entity.Config;
using System.Data.Entity.Infrastructure;

namespace Future.Data
{
    public class FutureDbConfiguration : DbConfiguration
    {
        public FutureDbConfiguration()
        {
            SetDefaultConnectionFactory(new SqlConnectionFactory(""));
        }
    }

    [DbConfigurationType(typeof(FutureDbConfiguration))]
    public class FutureObjectContext : DbContext, IDbContext
    {
        public FutureObjectContext(string connectionString)
            : base(connectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CountryRegionMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
