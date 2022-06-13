using FluentValidation;

namespace Inventory.Application.Features.Products.Commands.RemoveProductByName
{
    public class RemoveProductByNameCommandValidator : AbstractValidator<RemoveProductByNameCommand>
    {
        /// <summary>
        /// Using constructor to provide the properties validations
        /// </summary>
        public RemoveProductByNameCommandValidator()
        {
            RuleFor(p => p.ProductName)
                .NotNull()
                .NotEmpty().WithMessage("{ProductName} is required.");
        }
    }
}
