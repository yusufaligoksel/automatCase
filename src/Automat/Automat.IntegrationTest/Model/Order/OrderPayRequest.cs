using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat.IntegrationTest.Model.Order
{
    public class OrderPayRequest
    {
        public string ProcessId { get; set; }
        public decimal PaidMoney { get; set; }
    }
}
