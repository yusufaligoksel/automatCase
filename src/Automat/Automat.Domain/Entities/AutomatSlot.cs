using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat.Domain.Entities
{
    public class AutomatSlot : BaseEntity
    {
        public AutomatSlot()
        {
            this.AutomatSlotProducts = new List<AutomatSlotProduct>();
            this.ShoppingCarts = new List<ShoppingCart>();
        }
        public int SlotNumber { get; set; }
        public virtual IEnumerable<AutomatSlotProduct> AutomatSlotProducts { get; set; }
        public virtual IEnumerable<ShoppingCart> ShoppingCarts { get; set; }
    }
}
