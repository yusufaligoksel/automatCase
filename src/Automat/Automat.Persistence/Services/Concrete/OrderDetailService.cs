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
    public class OrderDetailService:BaseService<OrderDetail>, IOrderDetailService
    {
        private readonly IRepository<OrderDetail> _repository;
        public OrderDetailService(IRepository<OrderDetail> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
