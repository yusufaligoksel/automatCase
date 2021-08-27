using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Order()
        {
            this.OrderDetails = new List<OrderDetail>();
        }

        public Guid OrderCode { get; set; }
        public Guid ProcessId { get; set; }
        public byte OrderStatus { get; set; }
        public int PaymentTypeOptionId { get; set; }
        public int AutomatSlotId { get; set; }
        public decimal PaymentTotal { get; set; }
        public decimal? RefundAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual IEnumerable<OrderDetail> OrderDetails { get; set; }
        public PaymentTypeOption PaymentTypeOption { get; set; }
        public AutomatSlot AutomatSlot { get; set; }
    }
}
