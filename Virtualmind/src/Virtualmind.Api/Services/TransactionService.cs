using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Virtualmind.Api.Data;
using Virtualmind.Api.Exceptions;
using Virtualmind.Api.Model;
using Virtualmind.Api.Model.enums;
using Virtualmind.Api.Services.Interfaces;

namespace Virtualmind.Api.Services
{
    public class TransactionService : ITransactionService
    {

        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Transaction> AddAsync(Transaction entity)
        {
            await ValidateTransactionAsync(entity.UserId, entity.CurrencyCode, DateTime.Now.Month);
            var added = await _unitOfWork.TransactionRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();

            return added;
        }

        private async Task<bool> ValidateTransactionAsync(string userId, string supportedCurrencies, int mounth)
        {
            if (!SupportedCurrencies.IsValidCurrency(supportedCurrencies)) {
                throw new UnsupportedCurrencyException("The selected currency is not currently supported. Please, select USD or BRL.");
            }

            var transactions = await _unitOfWork.TransactionRepository.CurrentAmount(mounth, userId, supportedCurrencies.ToString());
            if (SupportedCurrencies.BRL.Equals(supportedCurrencies) && transactions >= 300.0m) {
                throw new BusinessException("The requested amount exceeds the monthly limit for the requested currency (BRL).");
            } else if (SupportedCurrencies.USD.Equals(supportedCurrencies) && transactions >= 200.0m)
            {
                throw new BusinessException("The requested amount exceeds the monthly limit for the requested currency (USD).");
            }
            return false;
          
        }


        public async Task DeleteAsync(string id)
        {
            var item = await FindByIdAsync(id);

            if (item == null)
            {
                throw new NotFoundException(id);
            }

            await _unitOfWork.TransactionRepository.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
        }

        public async Task<Transaction> FindByIdAsync(string Id)
        {
            return await _unitOfWork.TransactionRepository.FindByIdAsync(Id);
        }

        public Task UpdateAsync(Transaction entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Transaction>> FindAll()
        {
            return await _unitOfWork.TransactionRepository.FindAll();
        }
    }
}
