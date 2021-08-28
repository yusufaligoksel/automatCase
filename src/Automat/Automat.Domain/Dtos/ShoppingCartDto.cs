using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat.Domain.Dtos
{
    public class ShoppingCartDto
    {
        public Guid ProcessId { get; set; }
        public int AutomatSlotId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int? CategoryFeatureOptionId { get; set; }
        public int? FeatureOptionQuantity { get; set; }
        public int? PaymentTypeOptionId { get; set; }
    }
}
