using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat.Domain.Entities
{
    public class Category : BaseEntity
    {
        public Category()
        {
            this.Products = new List<Product>();
        }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public bool IsDeleted { get; set; }
        public virtual IEnumerable<Product> Products { get; set; }
    }
}
