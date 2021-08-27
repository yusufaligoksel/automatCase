using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Automat.Domain.Dtos
{
    public class OrderDto
    {
        public Guid OrderCode { get; set; }
        public DateTime OrderDate { get; set; }
        public string Message { get; set; }
        public decimal? RefundAmount { get; set; }
        public decimal PaymentTotal { get; set; }
        public OrderProductDto Product { get; set; }
        public OrderPaymentMethodDto PaymentMethod { get; set; }

    }

    public class OrderProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int Quantity { get; set; }
        public int? FeatureOptionId { get; set; }
        public string FeatureName { get; set; }
        public int? FeatureQuantity { get; set; }
    }

    public class OrderPaymentMethodDto
    {
        public int PaymentTypeId { get; set; }
        public string PaymentTypeName { get; set; }
        public int PaymentTypeOptionId { get; set; }
        public string PaymentTypeOptionName { get; set; }
    }
}
