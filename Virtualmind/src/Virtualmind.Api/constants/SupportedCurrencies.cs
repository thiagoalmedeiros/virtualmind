using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Virtualmind.Api.Model.enums
{
    public static class SupportedCurrencies
    {

        public static bool IsValidCurrency(string currency)
        {
            List<string> currencys = new List<string>() { USD, BRL };
            var s = currencys.Where(x => x.Equals(currency)).FirstOrDefault();
            return s != null;
        }

        public const string USD = "USD";
        public const string BRL = "BRL";

    }
}
