using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat.IntegrationTest.Model.ShoppingCart
{
    public class SelectProductQuantityRequest
    {
        public string ProcessId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
