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
        [ForeignKey("CategoryFeatureOptionId")]
        public int CategoryFeatureOptionId { get; set; }
        public int? Quantity { get; set; }
        public virtual OrderDetail OrderDetail { get; set; }
        public virtual CategoryFeatureOption CategoryFeatureOption { get; set; }

    }
}
