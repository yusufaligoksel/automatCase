using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat.Domain.Dtos
{
    public class SelectQuantityResultDto
    {
        public Guid ProcessId { get; set; }
        public string Message { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}
