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
    public class CategoryFeatureService:BaseService<CategoryFeature>, ICategoryFeatureService
    {
        private readonly IRepository<CategoryFeature> _repository;
        public CategoryFeatureService(IRepository<CategoryFeature> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
