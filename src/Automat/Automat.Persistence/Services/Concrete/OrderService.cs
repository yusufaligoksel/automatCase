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
    public class OrderService:BaseService<Order>, IOrderService
    {
        private readonly IRepository<Order> _repository;
        public OrderService(IRepository<Order> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Order> GetById(int id)
        {
            return await _repository.Table
                .Include(x => x.OrderDetails).ThenInclude(x=>x.Product.Category)
                .Include(x=>x.OrderDetails).ThenInclude(z=>z.OrderProductFeatureOptions).ThenInclude(y=>y.CategoryFeatureOption)
                .Include(x=>x.PaymentTypeOption).ThenInclude(x=>x.PaymentType)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
