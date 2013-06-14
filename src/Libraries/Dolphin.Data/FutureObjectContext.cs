using System;
using Dolphin.Data.Mapping.Course;
using Dolphin.Data.Mapping.Person;
using Dolphin.Data.Mapping.Sales;
using System.Data.Entity;
using System.Data.Entity.Config;
using System.Data.Entity.Infrastructure;

namespace Dolphin.Data
{
    /* This section is for Entity Framework 6*/
    public class DolphinDbConfiguration : DbConfiguration
    {
        public DolphinDbConfiguration()
        {
            SetDefaultConnectionFactory(new SqlConnectionFactory(""));
        }
    }

    [DbConfigurationType(typeof(DolphinDbConfiguration))]
    public class DolphinObjectContext : DbContext, IDbContext
    {
        public DolphinObjectContext(string connectionString, bool lazyLoad = true)
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

    [DbConfigurationType(typeof(DolphinDbConfiguration))]
    public class CourseObjectContext : DbContext, IDbContext
    {
        public CourseObjectContext(string connectionString, bool lazyLoad = true)
            : base(connectionString)
        {
            ((IObjectContextAdapter)this).ObjectContext.ContextOptions.LazyLoadingEnabled = lazyLoad;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CourseUnitMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
