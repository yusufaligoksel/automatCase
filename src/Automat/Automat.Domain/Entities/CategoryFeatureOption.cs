using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat.Domain.Entities
{
    public class CategoryFeatureOption : BaseEntity
    {
        public CategoryFeatureOption()
        {
            this.ShoppingCarts = new List<ShoppingCart>();
            this.OrderProductFeatureOptions = new List<OrderProductFeatureOption>();
        }
        [ForeignKey("CategoryFeatureId")]
        public int CategoryFeatureId { get; set; }
        /// <summary>
        /// sekerli /sekersiz
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 2-3 seker secebilme izni gibi
        /// </summary>
        public bool IsSelectQuantity { get; set; }
        public CategoryFeature CategoryFeature { get; set; }
        public virtual IEnumerable<OrderProductFeatureOption> OrderProductFeatureOptions { get; set; }
        public virtual IEnumerable<ShoppingCart> ShoppingCarts { get; set; }
    }
}
