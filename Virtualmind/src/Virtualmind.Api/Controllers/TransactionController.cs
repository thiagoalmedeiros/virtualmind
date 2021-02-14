using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Virtualmind.Api.Model;
using Virtualmind.Api.Services.Interfaces;

namespace Virtualmind.Api.Controllers
{
    [Route("api/[controller]")]
    public class TransactionController: MainController
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> FindAll()
        {
            return Ok(await _transactionService.FindAll());
        }

        [HttpPost]
        public async Task<ActionResult<Transaction>> GetQuotation(Transaction transaction)
        {
            transaction.Id = Guid.NewGuid().ToString();
            return await _transactionService.AddAsync(transaction);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            await _transactionService.DeleteAsync(id);

            return NoContent();
        }
    }
}
