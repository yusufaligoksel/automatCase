using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automat.Domain.Entities;
using Automat.Infrastructure.Repository;
using Automat.Persistence.Services.Abstract;

namespace Automat.Persistence.Services.Concrete
{
    public class ShoppingCartService : BaseService<ShoppingCart>, IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _repository;
        public ShoppingCartService(IRepository<ShoppingCart> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
