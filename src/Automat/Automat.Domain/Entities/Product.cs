using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat.Domain.Entities
{
    public class Product : BaseEntity
    {
        public Product()
        {
            this.AutomatSlotProducts = new List<AutomatSlotProduct>();
        }
        public string Name { get; set; }
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
        public Category Category { get; set; }
        public virtual IEnumerable<AutomatSlotProduct> AutomatSlotProducts { get; set; }
    }
}
