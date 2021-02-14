using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Virtualmind.Api.Exceptions;
using Virtualmind.Api.Model;
using Virtualmind.Api.Model.enums;
using Virtualmind.Api.Services.Interfaces;

namespace Virtualmind.Api.Services
{
    public class QuotationService : IQuotationService
    {
        public async Task<Quotation> GetQuotation(string currency)
        {
            Quotation value;
            if (SupportedCurrencies.BRL.Equals(currency))
            {
                value = await GetBRLQuotation();
            }
            else if (SupportedCurrencies.USD.Equals(currency))
            {
                value = await GetUSDQuotation();
            }
            else {
                throw new UnsupportedCurrencyException("The selected currency is not currently supported. Please, select USD or BRL.");
            }
            return value;
        }


        public async Task<Quotation> GetBRLQuotation()
        {
            Quotation quotation = new Quotation
            {
                Currency = SupportedCurrencies.BRL,
                Value = 0
            };
            using (var httpClient = new HttpClient())
            {
                using var response = await httpClient.GetAsync("https://www.bancoprovincia.com.ar/Principal/Dolar");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var reservationList = JsonConvert.DeserializeObject<List<string>>(apiResponse);
                    quotation.Value = Convert.ToDecimal(reservationList[0])/4.0m;
                }
            }
            return quotation;
     
        }

        public async Task<Quotation> GetUSDQuotation()
        {
            Quotation quotation = new Quotation
            {
                Currency = SupportedCurrencies.USD,
                Value = 0
            };
            using (var httpClient = new HttpClient())
            {
                using var response = await httpClient.GetAsync("https://www.bancoprovincia.com.ar/Principal/Dolar");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var reservationList = JsonConvert.DeserializeObject<List<string>>(apiResponse);
                    quotation.Value = Convert.ToDecimal(reservationList[0]);
                }
            }
            return quotation;
        }

        public async Task<List<Quotation>> GetAllQuotation()
        {
            var allQuotations = new List<Quotation>
            {
                await GetBRLQuotation(),
                await GetUSDQuotation()
            };

            return allQuotations;
        }

    }




}
