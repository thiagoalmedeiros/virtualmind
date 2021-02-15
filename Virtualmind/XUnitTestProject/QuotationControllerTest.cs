using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Virtualmind.Api.Controllers;
using Virtualmind.Api.Model;
using Virtualmind.Api.Model.enums;
using Virtualmind.Api.Services.Interfaces;
using Xunit;

namespace XUnitTestProject
{


    public class QuotationControllerTest
    {

        private QuotationController quotationController;

        public QuotationControllerTest() {
            SetupServiceMock();
        }

        private void SetupServiceMock()
        {
            var userServiceMock = new Mock<IQuotationService>();

            List<Quotation> list = new List<Quotation> { new Quotation { Currency = SupportedCurrencies.BRL, Value = 22.2m }, new Quotation { Currency = SupportedCurrencies.USD, Value = 87.3m } };

            userServiceMock.Setup(x => x.GetAllQuotation()).ReturnsAsync(list);

            userServiceMock.Setup(x => x.GetQuotation("BRL")).ReturnsAsync(new Quotation { Currency = SupportedCurrencies.BRL, Value = 22.2m });

            quotationController = new QuotationController(userServiceMock.Object);
        }


        [Fact]
        public async Task QuotationAllTest()
        {
            var result = await quotationController.GetQuotationAll();

            var contentResult = (OkObjectResult)result;
            var uquotationList = (List<Quotation>)contentResult.Value;

            Assert.Equal(2, uquotationList.Count);
            Assert.Equal(200, contentResult.StatusCode);
        }


        [Fact]
        public async Task QuotationNotSupportedCurrency()
        {
            var result = await quotationController.GetQuotation("ARG");

            var contentResult = (OkObjectResult)result;

            Assert.Null(contentResult.Value);
        }


        [Fact]
        public async Task QuotationBRLSupportedCurrency()
        {
            var result = await quotationController.GetQuotation("BRL");

            var contentResult = (OkObjectResult)result;

            var quotation = (Quotation)contentResult.Value;

            Assert.Equal("BRL", quotation.Currency);
        }


    }
}
