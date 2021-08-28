using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Automat.Application.Handlers.Product.Commands.Update;
using Automat.Domain.Dtos;
using Automat.Persistence.Services.Abstract;
using FluentValidation;
using MediatR;
using SharedLibrary.Response;

namespace Automat.Application.Handlers.Product.Commands.Update
{
    public class UpdateProductCommand : IRequest<GenericResponse<ProductDto>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, GenericResponse<ProductDto>>
        {
            private IValidator<UpdateProductCommand> _updateProductValidator;
            private readonly IProductService _productService;
            private readonly IMapper _mapper;
            public UpdateProductCommandHandler(IProductService productService,
                IValidator<UpdateProductCommand> updateProductValidator,
                IMapper mapper)
            {
                _updateProductValidator = updateProductValidator;
                _productService = productService;
                _mapper = mapper;
            }
            public async Task<GenericResponse<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                try
                {

                    #region Validation
                    var addProductValidResult = _updateProductValidator.Validate(request);
                    if (!addProductValidResult.IsValid)
                    {
                        Dictionary<string, string> errors = new Dictionary<string, string>();
                        foreach (var item in addProductValidResult.Errors)
                            errors.Add(item.PropertyName, item.ErrorMessage);

                        ErrorResult error = new(errors);
                        return GenericResponse<ProductDto>.ErrorResponse(error, statusCode: 400);
                    }
                    #endregion

                    var existingProduct = await _productService.FindAsync(request.Id);

                    if (existingProduct != null)
                    {
                        existingProduct.Name = request.Name;
                        existingProduct.CategoryId = request.CategoryId;
                        existingProduct.Price = request.Price;
                        existingProduct.ModifiedDate = DateTime.Now;

                        await _productService.UpdateAsync(existingProduct);
                    }
                    else
                    {
                        ErrorResult error = new("Güncellemek istediğiniz ürün bulunamadı.");
                        return GenericResponse<ProductDto>.ErrorResponse(error, statusCode: 400);
                    }

                    var result = _mapper.Map<ProductDto>(existingProduct);

                    return GenericResponse<ProductDto>.SuccessResponse(result, 200, "Ürün bilgisi başarıyla güncellendi.");
                }
                catch (Exception ex)
                {
                    ErrorResult error = new(ex.Message);
                    return GenericResponse<ProductDto>.ErrorResponse(error, statusCode: 500);
                }
            }
        }
    }
}
