namespace Inventory.Application.Features.Products.Queries.GetAllProducts
{
    /// <summary>
    /// DTO object with the Product information customized for the presentation layer
    /// </summary>
    public class ProductDTO
    {
        /// <summary>
        /// Product comercial name
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Product reference
        /// </summary>
        public string? Reference { get; set; }
        /// <summary>
        /// Type of product
        /// </summary>
        public string? Type { get; set; }
        /// <summary>
        /// Base price of the product. Guidance in order to compare prices with different suppliers
        /// </summary>
        public decimal? BasePrice { get; set; }
        /// <summary>
        /// Product manufacturer
        /// </summary>
        public string? Manufacturer { get; set; }
        /// <summary>
        /// Number of units
        /// </summary>
        public int NumUnits { get; set; }
        /// <summary>
        /// Product supplier
        /// </summary>
        public string? Supplier { get; set; }
        /// <summary>
        /// Product expiration date
        /// </summary>
        public DateTime? ExpirationDate { get; set; }

    }
}
