using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Virtualmind.Api.Exceptions
{
    public class NotFoundException : BusinessException
    {
        public string Id { get; private set; }

        public NotFoundException(string Id) : base($"Resource Not Found with the identifier {Id}")
        {
            Title = "Not Found";
        }
    }
}
