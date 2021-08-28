using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat.Domain.Dtos
{
    public class PaymentTypeOptionDto
    {
        public PaymentTypeOptionDto(int id, int paymentTypeId, string paymentTypeName, string name, bool refundPaymentStatus)
        {
            Id = id;
            PaymentTypeId = paymentTypeId;
            PaymentTypeName = paymentTypeName;
            Name = name;
            RefundPaymentStatus = refundPaymentStatus;
        }
        public int Id { get; set; }
        public int PaymentTypeId { get; set; }
        public string PaymentTypeName { get; set; }
        public string Name { get; set; }
        public bool RefundPaymentStatus { get; set; }
    }
}
