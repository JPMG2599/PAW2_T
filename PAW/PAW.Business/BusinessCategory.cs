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
    public interface IBusinessCategory
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task<bool> SaveAsync(Category category);
        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<CategoryViewModel>> FilterAsync(string filter);
    }
    public class BusinessCategory(IRepositoryCategory repo) : IBusinessCategory
    {
        public Task<IEnumerable<Category>> GetAllAsync() => repo.ReadAsync();

        public Task<Category?> GetByIdAsync(int id) => repo.FindAsync(id);

        public async Task<bool> SaveAsync(Category category)
        {
            if (category.CategoryId > 0) return await repo.UpdateAsync(category);
            return await repo.CreateAsync(category);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var found = await repo.FindAsync(id);
            return found != null && await repo.DeleteAsync(found);
        }

        public async Task<IEnumerable<CategoryViewModel>> FilterAsync(string filter)
        {
            switch (filter)
            {
                case "not-system":
                    
                    return await repo.FilterAsync(c => c.ModifiedBy != "System");

                case "admin":
                   
                    return await repo.FilterAsync(c => c.ModifiedBy == "Admin");

                case "no-lastmodified-and-not-admin":
                    
                    return await repo.FilterAsync(c => c.LastModified == null && c.ModifiedBy != "Admin");

                default:
                    return Enumerable.Empty<CategoryViewModel>();
            }
        }
    }
}
