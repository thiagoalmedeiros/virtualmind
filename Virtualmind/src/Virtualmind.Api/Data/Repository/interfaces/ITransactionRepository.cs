
using System;
using System.Threading.Tasks;
using Virtualmind.Api.Data.Repository.IRepository;
using Virtualmind.Api.Model;
using Virtualmind.Api.Model.enums;

namespace Virtualmind.Api.Data.Repository.interfaces
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        Task<decimal> CurrentAmount(int mounth, string userId, string supportedCurrencies);
    }
}
