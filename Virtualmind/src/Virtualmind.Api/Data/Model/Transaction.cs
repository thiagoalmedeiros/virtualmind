using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Virtualmind.Api.Data.Model;

namespace Virtualmind.Api.Model
{
    public class Transaction : BaseEntity
    {


        [Required(ErrorMessage = "UserId is Required")]
        public string UserId { set; get; }

        [Column(TypeName = "decimal(18, 2)")]
        [Required(ErrorMessage = "Amount is Required")]
        public decimal Amount { set; get; }

        [Required(ErrorMessage = "CurrencyCode is Required")]
        public string CurrencyCode { set; get; }
    }
}
