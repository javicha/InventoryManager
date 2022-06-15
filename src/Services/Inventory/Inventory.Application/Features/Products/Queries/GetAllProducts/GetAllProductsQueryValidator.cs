using FluentValidation;

namespace Inventory.Application.Features.Products.Queries.GetAllProducts
{
    /// <summary>
    /// CQRS pattern: Pre processor behavior. Validator for GetAllProductsQuery (we use fluent validation)
    /// </summary>
    public class GetAllProductsQueryValidator : AbstractValidator<GetAllProductsQuery>
    {
        /// <summary>
        /// Using constructor to provide the properties validations
        /// </summary>
        public GetAllProductsQueryValidator()
        {
            RuleFor(p => p.Page)
                .GreaterThan(0).WithMessage("{Page} must be greater than zero.");

            RuleFor(p => p.Size)
                .GreaterThan(0).WithMessage("{Size} must be greater than zero.");
        }
    }
}
