using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Automat.Application.Handlers.Category.Commands.Insert;
using Automat.Domain.Dtos;
using Automat.Persistence.Services.Abstract;
using FluentValidation;
using MediatR;
using SharedLibrary.Response;

namespace Automat.Application.Handlers.Category.Commands.Update
{
    public class UpdateCategoryCommand : IRequest<GenericResponse<CategoryDto>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }

        public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, GenericResponse<CategoryDto>>
        {
            private IValidator<UpdateCategoryCommand> _updateCategoryValidator;
            private readonly ICategoryService _categoryService;
            private readonly IMapper _mapper;
            public UpdateCategoryCommandHandler(ICategoryService categoryService,
                IValidator<UpdateCategoryCommand> updateCategoryValidator,
                IMapper mapper)
            {
                _updateCategoryValidator = updateCategoryValidator;
                _categoryService = categoryService;
                _mapper = mapper;
            }
            public async Task<GenericResponse<CategoryDto>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
            {
                try
                {

                    #region Validation
                    var addCategoryValidResult = _updateCategoryValidator.Validate(request);
                    if (!addCategoryValidResult.IsValid)
                    {
                        Dictionary<string, string> errors = new Dictionary<string, string>();
                        foreach (var item in addCategoryValidResult.Errors)
                            errors.Add(item.PropertyName, item.ErrorMessage);

                        ErrorResult error = new(errors);
                        return GenericResponse<CategoryDto>.ErrorResponse(error, statusCode: 400);
                    }
                    #endregion

                    var existingCategory = await _categoryService.FindAsync(request.Id);

                    if (existingCategory!=null)
                    {
                        existingCategory.Name = request.Name;
                        existingCategory.ParentId = request.ParentId;
                        existingCategory.ModifiedDate=DateTime.Now;

                        await _categoryService.UpdateAsync(existingCategory);
                    }
                    else
                    {
                        ErrorResult error = new("Güncellemek istediğiniz kategori bulunamadı.");
                        return GenericResponse<CategoryDto>.ErrorResponse(error, statusCode: 400);
                    }

                    var result = _mapper.Map<CategoryDto>(existingCategory);

                    return GenericResponse<CategoryDto>.SuccessResponse(result, 200, "Kategori başarıyla güncellendi.");
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
