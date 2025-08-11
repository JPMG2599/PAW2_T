using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PAW.Models.ViewModels;
using PAW.Models;
using PAW.Repositories;

namespace PAW.Business;
public interface IBusinessInventory
{
    Task<IEnumerable<Inventory>> GetAllAsync();
    Task<Inventory?> GetByIdAsync(int id);
    Task<bool> SaveAsync(Inventory inventory);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<InventoryViewModel>> FilterAsync(Expression<Func<Inventory, bool>> predicate);
}

public class BusinessInventory(IRepositoryInventory repo) : IBusinessInventory
{
    public Task<IEnumerable<Inventory>> GetAllAsync() => repo.ReadAsync();
    public Task<Inventory?> GetByIdAsync(int id) => repo.FindAsync(id);
    public async Task<bool> SaveAsync(Inventory i)
        => i.InventoryId > 0 ? await repo.UpdateAsync(i) : await repo.CreateAsync(i);
    public async Task<bool> DeleteAsync(int id)
    {
        var found = await repo.FindAsync(id);
        return found != null && await repo.DeleteAsync(found);
    }
    public Task<IEnumerable<InventoryViewModel>> FilterAsync(Expression<Func<Inventory, bool>> p)
        => repo.FilterAsync(p);
}
