using Inventory.Application.Common.Enums;
using MediatR;

namespace Inventory.Application.Features.Products.Commands.AddProduct
{
    /// <summary>
    /// CQRS pattern:AddProductCommand command parameters
    /// </summary>
    public class AddProductCommand : IRequest<NewProductDTO>
    {
        /// <summary>
        /// Product comercial name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Product reference
        /// </summary>
        public string Reference { get; set; }
        /// <summary>
        /// Product description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Type of product
        /// </summary>
        public ProductTypeEnum? Type { get; set; }
        /// <summary>
        /// Base price of the product. Guidance in order to compare prices with different suppliers
        /// </summary>
        public decimal? BasePrice { get; set; }
        /// <summary>
        /// Product manufacturer
        /// </summary>
        public ProductManufacturerEnum? Manufacturer { get; set; }
        /// <summary>
        /// Number of units
        /// </summary>
        public int NumUnits { get; set; }
        /// <summary>
        /// Critical Stock. When the product reaches the number of units indicated in this property, a notification could be launched to create a new order for the product.
        /// </summary>
        public int? MinStock { get; set; }
        /// <summary>
        /// Product supplier
        /// </summary>
        public ProductSupplierEnum? Supplier { get; set; }
        /// <summary>
        /// Product receipt date
        /// </summary>
        public DateTime ReceiptDate { get; set; }
        /// <summary>
        /// Product opening date
        /// </summary>
        public DateTime? OpeningDate { get; set; }
        /// <summary>
        /// Product exhaustion date
        /// </summary>
        public DateTime? ExhaustionDate { get; set; }
        /// <summary>
        /// Product expiration date
        /// </summary>
        public DateTime? ExpirationDate { get; set; }
        /// <summary>
        /// User creating the record
        /// </summary>
        public string UserCreated { get; set; }
    }
}
