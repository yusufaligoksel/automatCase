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
    public class PaymentTypeOptionService:BaseService<PaymentTypeOption>, IPaymentTypeOptionService
    {
        private readonly IRepository<PaymentTypeOption> _repository;
        public PaymentTypeOptionService(IRepository<PaymentTypeOption> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
