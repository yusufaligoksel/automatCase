using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat.IntegrationTest.Model.Management
{
    public class AddPaymentTypeOptionRequest
    {
        public int PaymentTypeId { get; set; }
        public string Name { get; set; }
        public bool RefundPaymentStatus { get; set; }
    }
}
