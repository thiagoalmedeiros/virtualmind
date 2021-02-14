using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Virtualmind.Api.Data.Repository.interfaces;
using Virtualmind.Api.Model;
using Virtualmind.Api.Model.enums;

namespace Virtualmind.Api.Data.Repository
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(DataContext context) : base(context)
        {

        }

        public async Task<decimal> CurrentAmount(int mounth, string userId,  string supportedCurrencies)
        {
            var items = await DataContext.Transactions.Where(t => t.CreatedAt.Month == mounth && t.UserId == userId && supportedCurrencies.ToString() == t.CurrencyCode).ToListAsync();
            var response = 0.0m;
            items.ForEach(item =>
            {
                response += item.Amount;
            });
          
            return response;
        }

    }
}
