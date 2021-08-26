using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automat.Domain.Entities;
using Automat.Infrastructure.Repository;

namespace Automat.Persistence.Services.Abstract
{
    public interface IShoppingCartService : IBaseService<ShoppingCart>
    {
        Task<ShoppingCart> GetCartByProcessId(Guid processId);
    }
}
