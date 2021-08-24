using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat.Domain.Entities
{
    public class OrderProductFeatureOption : BaseEntity
    {
        [ForeignKey("OrderDetailId")]
        public int OrderDetailId { get; set; }
        [ForeignKey("FeatureOptionId")]
        public int FeatureOptionId { get; set; }
        public int? Quantity { get; set; }
        public OrderDetail OrderDetail { get; set; }
        public CategoryFeatureOption CategoryFeatureOption { get; set; }

    }
}
