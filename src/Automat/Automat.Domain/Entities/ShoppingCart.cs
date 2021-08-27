using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat.Domain.Entities
{
    public class ShoppingCart:BaseEntity
    {
        public Guid ProcessId { get; set; }
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        [ForeignKey("FeatureOptionId")]
        public int? FeatureOptionId { get; set; }
        public int? FeatureOptionQuantity { get; set; }
        public int? PaymentTypeOptionId { get; set; }
        public int SlotId { get; set; }
        public Product Product { get; set; }
        public CategoryFeatureOption CategoryFeatureOption { get; set; }
        public PaymentTypeOption PaymentTypeOption { get; set; }
        public AutomatSlot AutomatSlot { get; set; }
    }
}
