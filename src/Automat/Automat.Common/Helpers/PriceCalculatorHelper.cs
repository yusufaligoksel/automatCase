using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat.Common.Helpers
{
    public static class PriceCalculatorHelper
    {
        public static decimal CalculateRefundAmount(decimal paymentTotal, decimal paidMoney)
        {
            if (paidMoney > paymentTotal)
            {
                return paidMoney - paymentTotal;
            }
            else
            {
                throw new Exception("Yetersiz bakiye!");
            }
        }

        public static decimal CalculatePaymentTotal(decimal quantity, decimal unitPrice)
        {
            try
            {
                return quantity * unitPrice;
            }
            catch (Exception e)
            {
                throw new Exception("Toplam tutar hesaplanırken hata oluştu");
            }
        }
    }
}
