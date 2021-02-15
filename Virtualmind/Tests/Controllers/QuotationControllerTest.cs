using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Virtualmind.Api.Controllers;
using Virtualmind.Api.Model;
using Virtualmind.Api.Model.enums;
using Virtualmind.Api.Services.Interfaces;
using Xunit;

namespace Tests.Controllers
{
    public class QuotationControllerTest
    {
        private readonly QuotationController quotationController;
       

        public QuotationControllerTest()
        {
            var serviceQuotation = new Mock<IQuotationService>();


            List<Quotation> list = new List<Quotation>() { new Quotation { Currency = SupportedCurrencies.BRL, Value = 22.2m }, new Quotation { Currency = SupportedCurrencies.USD, Value = 87.2m } };
            serviceQuotation.Setup(x => x.GetAllQuotation()).ReturnsAsync(list);

            serviceQuotation.Setup(x => x.GetQuotation(SupportedCurrencies.BRL)).ReturnsAsync( new Quotation { Currency = SupportedCurrencies.BRL, Value = 22.2m });


            quotationController = new QuotationController(serviceQuotation.Object);
            
        }

        [Fact]
        public void some_tests()
        {
            Assert.True(true);
        }

        [Fact]
        public async Task QuotationGetAllValidTest()
        {
            var result = await  quotationController.GetQuotationAll();
            var contentResult = (OkObjectResult) result;
            Assert.Equal(201, contentResult.StatusCode);
        }

        [Fact]
        public async Task QuotationGetBRLValidTest()
        {
            var result = await quotationController.GetQuotation(SupportedCurrencies.BRL);
            var contentResult = (OkObjectResult)result;
            var quotationBRL = (Quotation)contentResult.Value;
            Assert.Equal(quotationBRL.Currency, SupportedCurrencies.BRL);
        }


        private void SetupServiceMock()
        {
            
        }
    }

}
