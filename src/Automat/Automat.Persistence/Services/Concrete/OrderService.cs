﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automat.Domain.Entities;
using Automat.Infrastructure.Repository;
using Automat.Persistence.Services.Abstract;

namespace Automat.Persistence.Services.Concrete
{
    public class OrderService:BaseService<Order>, IOrderService
    {
        private readonly IRepository<Order> _repository;
        public OrderService(IRepository<Order> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
