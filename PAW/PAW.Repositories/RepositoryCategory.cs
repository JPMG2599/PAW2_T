using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PAW.Models.ViewModels;
using PAW.Models;

namespace PAW.Repositories
{
    public interface IRepositoryCategory
    {
        Task<bool> UpsertAsync(Category entity, bool isUpdating);
        Task<bool> CreateAsync(Category entity);
        Task<bool> DeleteAsync(Category entity);
        Task<IEnumerable<Category>> ReadAsync();
        Task<Category?> FindAsync(int id);
        Task<bool> UpdateAsync(Category entity);
        Task<bool> UpdateManyAsync(IEnumerable<Category> entities);
        Task<bool> ExistsAsync(Category entity);
        Task<bool> CheckBeforeSavingAsync(Category entity);
        Task<IEnumerable<CategoryViewModel>> FilterAsync(Expression<Func<Category, bool>> predicate);
    }

    public class RepositoryCategory : RepositoryBase<Category>, IRepositoryCategory
    {
        public async Task<bool> CheckBeforeSavingAsync(Category entity)
        {
            var exists = await ExistsAsync(entity);
            return await UpsertAsync(entity, exists);
        }

        public async Task<IEnumerable<CategoryViewModel>> FilterAsync(Expression<Func<Category, bool>> predicate)
        {
            return await DbContext.Categories
                .Where(predicate)
                .Select(c => new CategoryViewModel
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    Description = c.Description,
                    LastModified = c.LastModified,
                    ModifiedBy = c.ModifiedBy
                })
                .ToListAsync();
        }

        public async new Task<bool> ExistsAsync(Category entity)
            => await DbContext.Categories.AnyAsync(x => x.CategoryId == entity.CategoryId);
    }
}

