using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Virtualmind.Api.Exceptions
{

    [Serializable]
    public class BusinessException : Exception
    {
        public string Title { get; set; }
        public BusinessException()
        {

        }

        public BusinessException(string message) : base(message)
        {

        }
        public BusinessException(string message, string title) : base(message)
        {
            Title = title;
        }

        public BusinessException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
