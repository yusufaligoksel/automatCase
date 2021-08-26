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
    public class PaymentTypeOptionService:BaseService<PaymentTypeOption>, IPaymentTypeOptionService
    {
        private readonly IRepository<PaymentTypeOption> _repository;
        public PaymentTypeOptionService(IRepository<PaymentTypeOption> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<PaymentTypeOption> GetById(int id)
        {
            return await _repository.Table.Include(x=>x.PaymentType).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
