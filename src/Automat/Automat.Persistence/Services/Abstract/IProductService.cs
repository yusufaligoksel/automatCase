using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automat.Domain.Entities;

namespace Automat.Persistence.Services.Abstract
{
    public interface IProductService:IBaseService<Product>
    {
        Task<Product> GetById(int id);
    }
}
