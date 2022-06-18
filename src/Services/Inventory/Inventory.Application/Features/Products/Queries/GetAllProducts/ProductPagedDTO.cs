namespace Inventory.Application.Features.Products.Queries.GetAllProducts
{
    /// <summary>
    /// DTO object with the Product information customized for the presentation layer
    /// </summary>
    public class ProductPagedDTO
    {
        /// <summary>
        /// Total number of products in inventory
        /// </summary>
        public int TotalProducts { get; set; }

        /// <summary>
        /// Current page retrieved
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// List of products retrieved
        /// </summary>
        public List<ProductDTO> Products { get; set; }

        public ProductPagedDTO(int totalProducts, int currentPage, int pageSize, List<ProductDTO> products)
        {
            TotalProducts = totalProducts;
            CurrentPage = currentPage;
            PageSize = pageSize;
            Products = products ?? throw new ArgumentNullException(nameof(products));
        }
    }

    /// <summary>
    /// Product information customized for the Presentation layer
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
        /// <summary>
        /// User who has registered the product in the database
        /// </summary>
        public string? UserCreated { get; set; }
    }

}
