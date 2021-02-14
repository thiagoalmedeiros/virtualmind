using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Virtualmind.Api.Model;
using Virtualmind.Api.Services.Interfaces;

namespace Virtualmind.Api.Controllers
{
    [Route("api/[controller]")]
    public class QuotationController : MainController
    {
        private readonly IQuotationService _quotationService;
        public QuotationController(IQuotationService quotationService) {
            _quotationService = quotationService;
        }

        [HttpGet("{currency}")]
        public async Task<ActionResult<Quotation>> GetQuotation(string currency)
        {
            return await _quotationService.GetQuotation(currency);
        }

        [HttpGet]
        public async Task<ActionResult<List<Quotation>>> GetQuotationAll()
        {
            return await _quotationService.GetAllQuotation();
        }
    }
}
