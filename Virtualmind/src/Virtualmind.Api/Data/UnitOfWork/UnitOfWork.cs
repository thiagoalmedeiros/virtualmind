using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Virtualmind.Api.Data.Repository;
using Virtualmind.Api.Data.Repository.interfaces;
using Virtualmind.Api.Model;

namespace Virtualmind.Api.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext DbContext { get; }
        private ITransactionRepository _transactionRepository;

        public UnitOfWork(IConfiguration configuration)
        {
            var cone = configuration.GetConnectionString("DefaultConnection");

            DbContext = new DataContext(GetOptions(cone));
        }

        public UnitOfWork(DbContextOptions<DataContext> dbContextOptions)
        {
            DbContext = new DataContext(dbContextOptions);
        }


        public async Task CommitAsync()
        {
            await DbContext.SaveChangesAsync();
        }

        private static DbContextOptions GetOptions(string connection)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connection).Options;
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }

        public void DiscardChanges()
        {
            foreach (var entry in DbContext.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }

        public ITransactionRepository TransactionRepository => _transactionRepository ?? (_transactionRepository = new TransactionRepository(DbContext));

    }
}
