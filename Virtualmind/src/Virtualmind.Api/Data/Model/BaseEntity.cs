using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Virtualmind.Api.Data.Model
{
    public abstract class BaseEntity
    {
        [Key]
        public string Id { set; get; }

        [Required(ErrorMessage = "CreatedAt is Required")]
        public DateTime CreatedAt { set; get; }
    }
}
