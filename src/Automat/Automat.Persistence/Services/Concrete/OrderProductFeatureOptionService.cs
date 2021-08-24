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
    public class OrderProductFeatureOptionService:BaseService<OrderProductFeatureOption>, IOrderProductFeatureOptionService
    {
        private readonly IRepository<OrderProductFeatureOption> _repository;
        public OrderProductFeatureOptionService(IRepository<OrderProductFeatureOption> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
