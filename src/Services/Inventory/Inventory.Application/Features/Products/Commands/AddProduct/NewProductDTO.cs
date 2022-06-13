namespace Inventory.Application.Features.Products.Commands.AddProduct
{
    /// <summary>
    /// DTO object with the Product information customized for the presentation layer
    /// </summary>
    public class NewProductDTO
    {
        /// <summary>
        /// Product identifier
        /// </summary>
        public int Id { get; set; }
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
        /// Product expiration date
        /// </summary>
        public DateTime? ExpirationDate { get; set; }

    }
}
