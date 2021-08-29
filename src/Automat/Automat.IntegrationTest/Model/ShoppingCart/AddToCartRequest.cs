using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat.IntegrationTest.Model.ShoppingCart
{
    public class AddToCartRequest
    {
        public int SlotId { get; set; }
        public int ProductId { get; set; }
        public int? FeatureOptionId { get; set; }
        public int? FeatureOptionQuantity { get; set; }
    }
}
