using FluentValidation;
using Inventory.Application.Contracts.Persistence;

namespace Inventory.Application.Features.Products.Commands.AddProduct
{
    /// <summary>
    /// CQRS pattern: Pre processor behavior. Validator for AddProductCommand (we use fluent validation)
    /// </summary>
    public class GetAllProductsQueryValidator : AbstractValidator<AddProductCommand>
    {
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// Using constructor to provide the properties validations
        /// </summary>
        public GetAllProductsQueryValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{Name} is required.")
                .MaximumLength(100).WithMessage("{Name} must no exceed 100 characters.")
                .MustAsync(async (name, cancellationToken) => !(await _productRepository.ExistByName(name))).WithMessage("A product with that name already exists in inventory"); //Example of async validation

            RuleFor(p => p.Reference)
                .NotEmpty().WithMessage("{Reference} is required.")
                .MaximumLength(50).WithMessage("{Reference} must no exceed 50 characters.");

            RuleFor(p => p.Type)
                .NotNull().WithMessage("{Type} is required.")
                .IsInEnum();

            RuleFor(p => p.NumUnits)
                .GreaterThan(0).WithMessage("{NumUnits} must be greater than zero.");
        }
    }
}
