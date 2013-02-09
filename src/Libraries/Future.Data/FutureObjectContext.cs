using Future.Data.Mapping.Directory;
using System.Data.Entity;

namespace Future.Data
{
    public class FutureObjectContext : DbContext, IDbContext
    {
        public FutureObjectContext(string connectionString) : base(connectionString)
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CountryRegionMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
