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
public interface IBusinessSupplier
{
    Task<IEnumerable<Supplier>> GetAllAsync();
    Task<Supplier?> GetByIdAsync(int id);
    Task<bool> SaveAsync(Supplier supplier);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<SupplierViewModel>> FilterAsync(Expression<Func<Supplier, bool>> predicate);
}

public class BusinessSupplier(IRepositorySupplier repo) : IBusinessSupplier
{
    public Task<IEnumerable<Supplier>> GetAllAsync() => repo.ReadAsync();
    public Task<Supplier?> GetByIdAsync(int id) => repo.FindAsync(id);
    public async Task<bool> SaveAsync(Supplier s)
        => s.SupplierId > 0 ? await repo.UpdateAsync(s) : await repo.CreateAsync(s);
    public async Task<bool> DeleteAsync(int id)
    {
        var found = await repo.FindAsync(id);
        return found != null && await repo.DeleteAsync(found);
    }
    public Task<IEnumerable<SupplierViewModel>> FilterAsync(Expression<Func<Supplier, bool>> p)
        => repo.FilterAsync(p);
}
