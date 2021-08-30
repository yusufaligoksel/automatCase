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
    public class CategoryFeatureOptionService : BaseService<CategoryFeatureOption>, ICategoryFeatureOptionService
    {
        private readonly IRepository<CategoryFeatureOption> _repository;
        public CategoryFeatureOptionService(IRepository<CategoryFeatureOption> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<CategoryFeatureOption> GetById(int id)
        {
            return await _repository.Table.Include(x => x.CategoryFeature).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> CheckCategoryFeatureOption(int id, int categoryId)
        {
            return await _repository.Table.Include(x => x.CategoryFeature).AnyAsync(x => x.Id == id && x.CategoryFeature.CategoryId == categoryId);
        }
    }
}
