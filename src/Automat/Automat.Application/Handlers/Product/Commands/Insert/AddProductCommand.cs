using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Automat.Application.Handlers.Product.Commands.Insert;
using Automat.Domain.Dtos;
using Automat.Persistence.Services.Abstract;
using FluentValidation;
using MediatR;
using SharedLibrary.Response;

namespace Automat.Application.Handlers.Product.Commands.Insert
{
    public class AddProductCommand : IRequest<GenericResponse<ProductDto>>
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }

        public class AddProductCommandHandler : IRequestHandler<AddProductCommand, GenericResponse<ProductDto>>
        {
            private IValidator<AddProductCommand> _addProductValidator;
            private readonly IProductService _productService;
            private readonly IMapper _mapper;
            public AddProductCommandHandler(IProductService productService,
                IValidator<AddProductCommand> addProductValidator,
                IMapper mapper)
            {
                _addProductValidator = addProductValidator;
                _productService = productService;
                _mapper = mapper;
            }
            public async Task<GenericResponse<ProductDto>> Handle(AddProductCommand request, CancellationToken cancellationToken)
            {
                try
                {

                    #region Validation
                    var addProductValidResult = _addProductValidator.Validate(request);
                    if (!addProductValidResult.IsValid)
                    {
                        Dictionary<string, string> errors = new Dictionary<string, string>();
                        foreach (var item in addProductValidResult.Errors)
                            errors.Add(item.PropertyName, item.ErrorMessage);

                        ErrorResult error = new(errors);
                        return GenericResponse<ProductDto>.ErrorResponse(error, statusCode: 400);
                    }

                    #endregion

                    Domain.Entities.Product product = new Domain.Entities.Product
                    {
                        Name = request.Name,
                        CategoryId = request.CategoryId,
                        Price = request.Price,
                        IsDeleted = false,
                        CreatedDate = DateTime.Now
                    };

                    await _productService.InsertAsync(product);

                    var result = _mapper.Map<ProductDto>(product);

                    return GenericResponse<ProductDto>.SuccessResponse(result, 200, "Ürün başarıyla eklendi.");
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
