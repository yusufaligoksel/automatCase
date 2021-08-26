using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat.Domain.Dtos
{
    public class SelectPaymentMethodResultDto
    {
        public Guid ProcessId { get; set; }
        public int PaymentTypeId { get; set; }
        public string PaymentTypeName { get; set; }
        public int PaymentTypeOptionId { get; set; }
        public string PaymentTypeOptionName { get; set; }
        public string Message { get; set; }
    }
}
