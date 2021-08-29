using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat.IntegrationTest.Model.Product
{
    public class NewCategoryRequest
    {
        public string Name { get; set; }
        public int ParentId { get; set; }
    }
}
