using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Automat.Domain.Dtos;
using Automat.Domain.Entities;

namespace Automat.Domain.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ShoppingCart, ShoppingCartDto>();
            CreateMap<Category, CategoryDto>();
        }
    }
}
