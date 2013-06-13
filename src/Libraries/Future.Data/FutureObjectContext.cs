using System;
using Future.Data.Mapping.Course;
using Future.Data.Mapping.Person;
using Future.Data.Mapping.Sales;
using System.Data.Entity;
using System.Data.Entity.Config;
using System.Data.Entity.Infrastructure;

namespace Future.Data
{
    /* This section is for Entity Framework 6*/
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
        public FutureObjectContext(string connectionString, bool lazyLoad = true)
            : base(connectionString)
        {
            ((IObjectContextAdapter)this).ObjectContext.ContextOptions.LazyLoadingEnabled = lazyLoad;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CountryRegionMap());
            modelBuilder.Configurations.Add(new TerritoryMap());

            base.OnModelCreating(modelBuilder);
        }
    }

    [DbConfigurationType(typeof(FutureDbConfiguration))]
    public class CourseObjectContext : DbContext, IDbContext
    {
        public CourseObjectContext(string connectionString, bool lazyLoad = true)
            : base(connectionString)
        {
            ((IObjectContextAdapter)this).ObjectContext.ContextOptions.LazyLoadingEnabled = lazyLoad;
            this.Database.Log = Console.WriteLine;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CourseUnitMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
