using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Linq;
using System.Threading.Tasks;
using Virtualmind.Api.Data.Model;

namespace Virtualmind.Api.Model
{
    public class DataContext : DbContext
    {
        public ModelBuilder ModelBuilder { get; private set; }

        public DataContext() : base(GetOptions())
        {

        }
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        #region DBSet
        public DbSet<Transaction> Transactions { get; set; }
        #endregion

        private static DbContextOptions GetOptions()
        {
            var connectionString = "Server=.\\SQLEXPRESS;Database=virtualmind;Trusted_Connection=True;MultipleActiveResultSets=true";

            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }


        public async Task<int> SaveChangesAsync()
        {
            AddTimestamps();
            return await base.SaveChangesAsync();
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedAt = DateTime.Now;
                    ((BaseEntity)entity.Entity).Id = Guid.NewGuid().ToString();
                }

            }
        }
    }
}
