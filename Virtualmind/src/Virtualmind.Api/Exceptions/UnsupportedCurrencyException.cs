
using System;

namespace Virtualmind.Api.Exceptions
{

     [Serializable]
    public class UnsupportedCurrencyException : Exception
    {
        public string Title { get; set; }
        public UnsupportedCurrencyException()
        {

        }

        public UnsupportedCurrencyException(string message) : base(message)
        {

        }
        public UnsupportedCurrencyException(string message, string title) : base(message)
        {
            Title = title;
        }

        public UnsupportedCurrencyException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
