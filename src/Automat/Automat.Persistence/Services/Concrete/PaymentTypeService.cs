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
    public class PaymentTypeService:BaseService<PaymentType>, IPaymentTypeService
    {
        private readonly IRepository<PaymentType> _repository;
        public PaymentTypeService(IRepository<PaymentType> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
