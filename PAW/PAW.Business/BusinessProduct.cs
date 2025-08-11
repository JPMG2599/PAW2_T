using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAW.Models.ViewModels;
using PAW.Models;
using PAW.Repositories;

namespace PAW.Business
{
    public interface IBusinessProduct
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<bool> SaveAsync(Product product);   
        Task<bool> DeleteAsync(int id);

       
        Task<IEnumerable<ProductViewModel>> FilterAsync(string filter);
    }
    public class BusinessProduct(IRepositoryProduct repo) : IBusinessProduct
    {
        public Task<IEnumerable<Product>> GetAllAsync() => repo.ReadAsync();

        public Task<Product?> GetByIdAsync(int id) => repo.FindAsync(id);

        public async Task<bool> SaveAsync(Product product)
        {
            if (product.ProductId > 0) return await repo.UpdateAsync(product);
            return await repo.CreateAsync(product);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var found = await repo.FindAsync(id);
            return found != null && await repo.DeleteAsync(found);
        }

        public async Task<IEnumerable<ProductViewModel>> FilterAsync(string filter)
        {
            var all = await repo.ReadAsync();
            if (!all.Any()) return Enumerable.Empty<ProductViewModel>();

            switch (filter)
            {
                case "no-supplierid":
                    return await repo.FilterAsync(p => p.SupplierId == null);

                case "no-inventoryid":
                    return await repo.FilterAsync(p => p.InventoryId == null);

                case "highest-rating":
                    {
                        var max = all.Where(p => p.Rating.HasValue)
                                     .Max(p => p.Rating);
                        return await repo.FilterAsync(p => p.Rating == max);
                    }

                case "lowest-rating":
                    {
                        var min = all.Where(p => p.Rating.HasValue)
                                     .Min(p => p.Rating);
                        return await repo.FilterAsync(p => p.Rating == min);
                    }

                case "most-common-rating":
                    {
                        var most = all.Where(p => p.Rating.HasValue)
                                      .GroupBy(p => p.Rating)
                                      .OrderByDescending(g => g.Count())
                                      .Select(g => g.Key)
                                      .FirstOrDefault();
                        return await repo.FilterAsync(p => p.Rating == most);
                    }

                default:
                    return Enumerable.Empty<ProductViewModel>();
            }
        }
    }
}
