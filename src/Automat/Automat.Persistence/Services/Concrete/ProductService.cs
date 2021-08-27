using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automat.Domain.Entities;
using Automat.Infrastructure.Repository;
using Automat.Persistence.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Automat.Persistence.Services.Concrete
{
    public class ProductService : BaseService<Product>, IProductService
    {
        private readonly IRepository<Product> _repository;
        public ProductService(IRepository<Product> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Product> GetById(int id)
        {
            return await _repository.Table.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
