using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat.Domain.Entities
{
    public class CategoryFeature:BaseEntity
    {
        public CategoryFeature()
        {
            this.CategoryFeatureOptions = new List<CategoryFeatureOption>();
        }

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        /// <summary>
        /// Ana title Şeker durumu
        /// </summary>
        public string Name { get; set; }
        public Category Category { get; set; }
        public virtual IEnumerable<CategoryFeatureOption> CategoryFeatureOptions { get; set; }

    }
}
