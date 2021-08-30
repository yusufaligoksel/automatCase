using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automat.Domain.Entities;

namespace Automat.Persistence.Services.Abstract
{
    public interface ICategoryFeatureOptionService:IBaseService<CategoryFeatureOption>
    {
        Task<CategoryFeatureOption> GetById(int id);
        Task<bool> CheckCategoryFeatureOption(int id, int categoryId);
    }
}
