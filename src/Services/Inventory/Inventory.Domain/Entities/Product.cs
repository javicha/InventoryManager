using Inventory.Domain.Common;

namespace Inventory.Domain.Entities
{
#pragma warning disable CS8618
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
        /// Product expiration date
        /// </summary>
        public DateTime? ExpirationDate { get; private set; }

        public Product() { }

        public Product(string name, string reference, string description, int? typeId, decimal? basePrice, int? manufacturerId, int numUnits,
            int? minStock, int? supplierId, DateTime receiptDate, DateTime? expirationDate,
            string userCreated)
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
            ExpirationDate = expirationDate;
            UserCreated = userCreated;
            UserModified = userCreated;
        }


        #region PublicMethods

        /*
         * For illustrative purposes only. We do not use these methods. 
         * They are an example of business logic encapsulated in the domain, following the DDD principles
         */

        /// <summary>
        /// Decrements the number of units of the product. The minimum number of units will be zero.
        /// </summary>
        /// <param name="quantity">Number of units to decrease</param>
        public void DecreaseNumUnits(int quantity)
        {
            NumUnits -= quantity;
            if (NumUnits < 0) { NumUnits = 0; }
        }

        /// <summary>
        /// Increase the number of units of the product
        /// </summary>
        /// <param name="quantity">Number of units to increase</param>
        public void IncreaseNumUnits(int quantity)
        {
            if (quantity > 0)
            {
                NumUnits += quantity;
            }
        }

        #endregion
    }
}
#pragma warning restore CS8618