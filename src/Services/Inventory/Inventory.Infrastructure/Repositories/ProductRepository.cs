using Inventory.Application.Contracts.Persistence;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(InventoryContext inventoryContext) : base(inventoryContext) { }

        public Task<bool> ExistByName(string productName)
        {
            string name = productName.Trim().ToLower();
            return _inventoryContext.Products.AnyAsync(x => x.Name.ToLower() == name && x.DateDeleted == null);
        }

        public Task<Product?> GetByName(string productName)
        {
            string name = productName.Trim().ToLower();
            return _inventoryContext.Products.Where(x => x.Name.ToLower() == name && x.DateDeleted == null).FirstOrDefaultAsync();
        }

        public async Task<Tuple<List<Product>, int>> GetAllProductsPagAsync(int startIndex, int count, string? text)
        {
            IQueryable<Product> resultTemp = _inventoryContext.Products.Where(p => p.DateDeleted == null).AsQueryable();

            if (!string.IsNullOrWhiteSpace(text))
            {
                string searchText = text.Trim().ToLower();
                resultTemp = resultTemp.Where(x => x.Name.ToLower().Contains(searchText) || x.Reference.ToLower().Contains(searchText));
            }

            int cnt = resultTemp.Distinct().Count();

            resultTemp = resultTemp.OrderBy(x => x.Name);
           
            resultTemp = resultTemp.Skip(count * (startIndex - 1)).Take(count);
            var result = new Tuple<List<Product>, int>(await resultTemp.ToListAsync(), cnt);

            return result;
        }

        public async Task<List<Product>> GetProductsExpiredByDateAsync(DateTime dateFrom, DateTime dateTo)
        {
            dateTo = dateTo.AddDays(1);

            var productsExpired = await GetAsync(p => p.ExpirationDate != null && p.ExpirationDate >= dateFrom && p.ExpirationDate <= dateTo);
            return productsExpired.ToList();
        }
    }
}
