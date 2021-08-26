using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat.Domain.Dtos
{
    public class CartResultDto
    {
        public Guid ProcessId { get; set; }
        public CartProductDto CartItem { get; set; }
        public string Message { get; set; }
    }

    public class CartProductDto
    {
        public CartProductDto(int productId, string productName, int? featureOptionId, string featureName, int? featureQuantity)
        {
            ProductId = productId;
            ProductName = productName;
            FeatureOptionId = featureOptionId;
            FeatureName = featureName;
            FeatureQuantity = featureQuantity;
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? FeatureOptionId { get; set; }
        public string FeatureName { get; set; }
        public int? FeatureQuantity { get; set; }

    }
}
