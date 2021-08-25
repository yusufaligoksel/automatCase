using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat.Common.Helpers
{
    public static class PriceCalculatorHelper
    {
        public static decimal CalculateRefundAmount(decimal PaymentTotal, decimal PaidMoney)
        {
            if (PaidMoney > PaymentTotal)
            {
                return PaidMoney - PaymentTotal;
            }
            else
            {
                throw new Exception("Yetersiz bakiye!");
            }
        }
    }
}
