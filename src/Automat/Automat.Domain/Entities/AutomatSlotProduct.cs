using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat.Domain.Entities
{
    public class AutomatSlotProduct : BaseEntity
    {
        [ForeignKey("SlotId")]
        public int SlotId { get; set; }
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public AutomatSlot AutomatSlot { get; set; }
        public Product Product { get; set; }
    }
}
