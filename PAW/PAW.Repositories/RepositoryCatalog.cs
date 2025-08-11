using Microsoft.EntityFrameworkCore;
using PAW.Models;
using PAW.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PAW.Repositories
{

    public interface IRepositoryCatalog
    {
        Task<bool> UpsertAsync(Catalog entity, bool isUpdating);
        Task<bool> CreateAsync(Catalog entity);
        Task<bool> DeleteAsync(Catalog entity);
        Task<IEnumerable<Catalog>> ReadAsync();
        Task<Catalog> FindAsync(int id);
        Task<bool> UpdateAsync(Catalog entity);
        Task<bool> UpdateManyAsync(IEnumerable<Catalog> entities);
        Task<bool> ExistsAsync(Catalog entity);
        Task<bool> CheckBeforeSavingAsync(Catalog entity);
        Task<IEnumerable<CatalogViewModel>> FilterAsync(Expression<Func<Catalog, bool>> predicate);
    }

    public class RepositoryCatalog : RepositoryBase<Catalog>, IRepositoryCatalog
    {
        public async Task<bool> CheckBeforeSavingAsync(Catalog entity)
        {
            var exists = await ExistsAsync(entity);
            if (exists) { };

            return await UpsertAsync(entity, exists);
        }

        public async Task<IEnumerable<CatalogViewModel>> FilterAsync(Expression<Func<Catalog, bool>> predicate)
        {
            var random = new Random();

            return await DbContext.Catalogs
                .Where(predicate)
                .Select(x => new CatalogViewModel
                {
                    Identifier = x.Identifier,
                    Name = x.Name,
                    Description = x.Description,
                    Rating = x.Rating,
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,
                    TempID = random.Next(1, 1000)
                })
                .ToListAsync();
        }

        public async new Task<bool> ExistsAsync(Catalog entity)
        {
            return await DbContext.Catalogs
                .AnyAsync(x => x.Identifier == entity.Identifier);
        }

    }
}

