using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Persistence
{
#pragma warning disable CS8618
    /// <summary>
    /// We use EntityFrameworkCore InMemory as ORM system. Our data context must inherit from DbContext, which in turn belongs to EntityframeworkCore
    /// </summary>
    public class InventoryContext : DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
#pragma warning restore CS8618