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
    public interface IRepositoryInventory
    {
        Task<bool> UpsertAsync(Inventory entity, bool isUpdating);
        Task<bool> CreateAsync(Inventory entity);
        Task<bool> DeleteAsync(Inventory entity);
        Task<IEnumerable<Inventory>> ReadAsync();
        Task<Inventory?> FindAsync(int id);
        Task<bool> UpdateAsync(Inventory entity);
        Task<bool> UpdateManyAsync(IEnumerable<Inventory> entities);
        Task<bool> ExistsAsync(Inventory entity);
        Task<bool> CheckBeforeSavingAsync(Inventory entity);
        Task<IEnumerable<InventoryViewModel>> FilterAsync(Expression<Func<Inventory, bool>> predicate);
    }

    public class RepositoryInventory : RepositoryBase<Inventory>, IRepositoryInventory
    {
        public async Task<bool> CheckBeforeSavingAsync(Inventory entity)
        {
            var exists = await ExistsAsync(entity);
            return await UpsertAsync(entity, exists);
        }

        public async Task<IEnumerable<InventoryViewModel>> FilterAsync(Expression<Func<Inventory, bool>> predicate)
        {
            return await DbContext.Inventories
                .Where(predicate)
                .Select(i => new InventoryViewModel
                {
                    InventoryId = i.InventoryId,
                    UnitPrice = i.UnitPrice,
                    UnitsInStock = i.UnitsInStock,
                    LastUpdated = i.LastUpdated,
                    DateAdded = i.DateAdded,
                    ModifiedBy = i.ModifiedBy
                })
                .ToListAsync();
        }

        public async new Task<bool> ExistsAsync(Inventory entity)
            => await DbContext.Inventories.AnyAsync(x => x.InventoryId == entity.InventoryId);
    }
}
