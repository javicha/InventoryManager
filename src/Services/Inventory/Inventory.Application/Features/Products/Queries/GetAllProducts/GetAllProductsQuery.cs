using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Application.Features.Products.Queries.GetAllProducts
{
    /// <summary>
    /// CQRS pattern:GetAllProductsQuery query parameters
    /// </summary>
    public class GetAllProductsQuery : IRequest<ProductPagedDTO>
    {
        /// <summary>
        /// List search filter. Applies to the name and reference of the product
        /// </summary>
        public string? FilterText { get; set; }

        /// <summary>
        /// Page to retrieve
        /// </summary>
        [Required]
        public int Page { get; set; }

        /// <summary>
        /// Page size
        /// </summary>
        [Required]
        public int Size { get; set; }

        public GetAllProductsQuery() { }

        public GetAllProductsQuery(string filterText, int page, int size)
        {
            FilterText = filterText ?? throw new ArgumentNullException(nameof(filterText));
            Page = page;
            Size = size;
        }
    }
}
