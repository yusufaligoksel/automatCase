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
    public class AutomatSlotProductService:BaseService<AutomatSlotProduct>, IAutomatSlotProductService
    {
        private readonly IRepository<AutomatSlotProduct> _repository;
        public AutomatSlotProductService(IRepository<AutomatSlotProduct> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<bool> CheckAutomatSlotProduct(int slotId, int productId)
        {
            return await _repository.Table.AnyAsync(x => x.AutomatSlotId == slotId && x.ProductId == productId);
        }
    }
}
