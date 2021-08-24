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
    public class AutomatSlotService:BaseService<AutomatSlot>, IAutomatSlotService
    {
        private readonly IRepository<AutomatSlot> _repository;
        public AutomatSlotService(IRepository<AutomatSlot> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
