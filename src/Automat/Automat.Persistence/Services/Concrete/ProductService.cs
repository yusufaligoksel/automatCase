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
    public class ProductService:BaseService<Product>, IProductService
    {
        private readonly IRepository<Product> _repository;
        public ProductService(IRepository<Product> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
