using System.Collections.Generic;
using System.Threading.Tasks;
using Virtualmind.Api.Model;

namespace Virtualmind.Api.Services.Interfaces
{
    public interface IQuotationService
    {

        public Task<Quotation> GetQuotation(string currency);
        Task<List<Quotation>> GetAllQuotation();
    }
}
