using System;
using System.Threading.Tasks;
using Virtualmind.Api.Data.Repository.interfaces;

namespace Virtualmind.Api.Data
{
    public interface IUnitOfWork : IDisposable
    {
        ITransactionRepository TransactionRepository { get; }

        Task CommitAsync();
        void DiscardChanges();
    }
}
