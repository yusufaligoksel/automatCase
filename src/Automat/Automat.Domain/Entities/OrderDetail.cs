using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat.Domain.Entities
{
    public class OrderDetail : BaseEntity
    {
        public OrderDetail()
        {
            this.OrderProductFeatureOptions = new List<OrderProductFeatureOption>();
        }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
        public virtual IEnumerable<OrderProductFeatureOption> OrderProductFeatureOptions { get; set; }
    }
}
