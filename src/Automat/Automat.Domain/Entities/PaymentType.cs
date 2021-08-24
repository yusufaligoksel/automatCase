using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat.Domain.Entities
{
    public class PaymentType : BaseEntity
    {
        public PaymentType()
        {
            this.PaymentTypeOptions = new List<PaymentTypeOption>();
        }
        public string Name { get; set; }
        public virtual IEnumerable<PaymentTypeOption> PaymentTypeOptions { get; set; }
    }
}
