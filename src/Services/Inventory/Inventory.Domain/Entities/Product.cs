using Inventory.Domain.Common;

namespace Inventory.Domain.Entities
{
    /// <summary>
    /// Entity that models a laboratory consumable product
    /// </summary>
    public class Product : EntityBase
    {
        /// <summary>
        /// Product comercial name
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Product reference
        /// </summary>
        public string Reference { get; private set; }
        /// <summary>
        /// Product description
        /// </summary>
        public string Description { get; private set; }
        /// <summary>
        /// Type of product
        /// </summary>
        public int? TypeId { get; private set; }
        /// <summary>
        /// Base price of the product. Guidance in order to compare prices with different suppliers
        /// </summary>
        public decimal? BasePrice { get; private set; }
        /// <summary>
        /// Product manufacturer
        /// </summary>
        public int? ManufacturerId { get; private set; }
        /// <summary>
        /// Number of units
        /// </summary>
        public int NumUnits { get; private set; }
        /// <summary>
        /// Critical Stock. When the product reaches the number of units indicated in this property, a notification could be launched to create a new order for the product.
        /// </summary>
        public int? MinStock { get; private set; }
        /// <summary>
        /// Product supplier
        /// </summary>
        public int? SupplierId { get; private set; }
        /// <summary>
        /// Product receipt date
        /// </summary>
        public DateTime ReceiptDate { get; private set; }
        /// <summary>
        /// Product opening date
        /// </summary>
        public DateTime? OpeningDate { get; private set; }
        /// <summary>
        /// Product exhaustion date
        /// </summary>
        public DateTime? ExhaustionDate { get; private set; }
        /// <summary>
        /// Product expiration date
        /// </summary>
        public DateTime? ExpirationDate { get; private set; }

        public Product(string name, string reference, string description, int? typeId, decimal? basePrice, int? manufacturerId, int numUnits, 
            int? minStock, int? supplierId, DateTime receiptDate, DateTime? openingDate, DateTime? exhaustionDate, DateTime? expirationDate,
            string userCreated) : base(userCreated)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Reference = reference ?? throw new ArgumentNullException(nameof(reference));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            TypeId = typeId;
            BasePrice = basePrice;
            ManufacturerId = manufacturerId;
            NumUnits = numUnits;
            MinStock = minStock;
            SupplierId = supplierId;
            ReceiptDate = receiptDate;
            OpeningDate = openingDate;
            ExhaustionDate = exhaustionDate;
            ExpirationDate = expirationDate;
        }
    }
}
