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
    public class CategoryFeatureOptionService:BaseService<CategoryFeatureOption>, ICategoryFeatureOptionService
    {
        private readonly IRepository<CategoryFeatureOption> _repository;
        public CategoryFeatureOptionService(IRepository<CategoryFeatureOption> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
