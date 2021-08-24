using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat.Domain.Entities
{
    public class PaymentTypeOption : BaseEntity
    {
        public string Name { get; set; }
        [ForeignKey("PaymentId")]
        public int PaymentId { get; set; }

        /// <summary>
        /// Coin/banknote flag
        /// </summary>
        public bool RefundPaymentStatus { get; set; }
        public PaymentType PaymentType { get; set; }
    }
}
