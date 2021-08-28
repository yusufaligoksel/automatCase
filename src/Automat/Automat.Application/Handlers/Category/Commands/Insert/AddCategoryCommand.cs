using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Automat.Domain.Dtos;
using Automat.Persistence.Services.Abstract;
using FluentValidation;
using MediatR;
using SharedLibrary.Response;

namespace Automat.Application.Handlers.Category.Commands.Insert
{
    public class AddCategoryCommand:IRequest<GenericResponse<CategoryDto>>
    {
        public string Name { get; set; }
        public int ParentId { get; set; }

        public class AddCategoryCommandHandler:IRequestHandler<AddCategoryCommand,GenericResponse<CategoryDto>>
        {
            private IValidator<AddCategoryCommand> _addCategoryValidator;
            private readonly ICategoryService _categoryService;
            private readonly IMapper _mapper;
            public AddCategoryCommandHandler(ICategoryService categoryService,
                IValidator<AddCategoryCommand> addCategoryValidator,
                IMapper mapper)
            {
                _addCategoryValidator = addCategoryValidator;
                _categoryService = categoryService;
                _mapper = mapper;
            }
            public async Task<GenericResponse<CategoryDto>> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
            {
                try
                {

                    #region Validation
                    var addCategoryValidResult = _addCategoryValidator.Validate(request);
                    if (!addCategoryValidResult.IsValid)
                    {
                        Dictionary<string, string> errors = new Dictionary<string, string>();
                        foreach (var item in addCategoryValidResult.Errors)
                            errors.Add(item.PropertyName, item.ErrorMessage);

                        ErrorResult error = new(errors);
                        return GenericResponse<CategoryDto>.ErrorResponse(error, statusCode: 400);
                    }
                    
                    #endregion

                    Domain.Entities.Category category = new Domain.Entities.Category
                    {
                        Name = request.Name,
                        ParentId = request.ParentId,
                        IsDeleted = false,
                        CreatedDate = DateTime.Now
                    };

                    await _categoryService.InsertAsync(category);

                    var result = _mapper.Map<CategoryDto>(category);

                    return GenericResponse<CategoryDto>.SuccessResponse(result, 200,"Kategori başarıyla eklendi.");
                }
                catch (Exception ex)
                {
                    ErrorResult error = new(ex.Message);
                    return GenericResponse<CategoryDto>.ErrorResponse(error, statusCode: 500);
                }
            }
        }
    }
}
