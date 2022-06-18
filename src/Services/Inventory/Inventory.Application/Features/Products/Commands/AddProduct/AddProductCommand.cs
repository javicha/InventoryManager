using Application.DTO;
using Inventory.Application.Common.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Application.Features.Products.Commands.AddProduct
{
    /// <summary>
    /// CQRS pattern:AddProductCommand command parameters
    /// </summary>
    public class AddProductCommand : CommandBase, IRequest<NewProductDTO>
    {
        /// <summary>
        /// Product comercial name
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Product reference
        /// </summary>
        [Required]
        public string Reference { get; set; }
        /// <summary>
        /// Product description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Type of product
        /// </summary>
        [Required]
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
        /// Product expiration date
        /// </summary>
        public DateTime? ExpirationDate { get; set; }

        public AddProductCommand(string name, string reference, string description, ProductTypeEnum? type, decimal? basePrice, ProductManufacturerEnum? manufacturer, 
            int numUnits, int? minStock, ProductSupplierEnum? supplier, DateTime receiptDate, DateTime? expirationDate)
        {
            Name = name;
            Reference = reference;
            Description = description;
            Type = type;
            BasePrice = basePrice;
            Manufacturer = manufacturer;
            NumUnits = numUnits;
            MinStock = minStock;
            Supplier = supplier;
            ReceiptDate = receiptDate;
            ExpirationDate = expirationDate;
        }
    }
}
