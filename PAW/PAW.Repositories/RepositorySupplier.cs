using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PAW.Models.ViewModels;
using PAW.Models;
using Microsoft.EntityFrameworkCore;

namespace PAW.Repositories
{
    public interface IRepositorySupplier
    {
        Task<bool> UpsertAsync(Supplier entity, bool isUpdating);
        Task<bool> CreateAsync(Supplier entity);
        Task<bool> DeleteAsync(Supplier entity);
        Task<IEnumerable<Supplier>> ReadAsync();
        Task<Supplier> FindAsync(int id);
        Task<bool> UpdateAsync(Supplier entity);
        Task<bool> UpdateManyAsync(IEnumerable<Supplier> entities);
        Task<bool> ExistsAsync(Supplier entity);
        Task<bool> CheckBeforeSavingAsync(Supplier entity);
        Task<IEnumerable<SupplierViewModel>> FilterAsync(Expression<Func<Supplier, bool>> predicate);
    }

    public class RepositorySupplier : RepositoryBase<Supplier>, IRepositorySupplier
    {
        public async Task<bool> CheckBeforeSavingAsync(Supplier entity)
        {
            var exists = await ExistsAsync(entity);
            if (exists) { }

            return await UpsertAsync(entity, exists);
        }

        public async Task<IEnumerable<SupplierViewModel>> FilterAsync(Expression<Func<Supplier, bool>> predicate)
        {
            return await DbContext.Suppliers
                .Where(predicate)
                .Select(s => new SupplierViewModel
                {
                    SupplierId = s.SupplierId,
                    SupplierName = s.SupplierName,
                    ContactName = s.ContactName,
                    ContactTitle = s.ContactTitle,
                    Phone = s.Phone,
                    Address = s.Address,
                    City = s.City,
                    Country = s.Country,
                    LastModified = s.LastModified,
                    ModifiedBy = s.ModifiedBy
                })
                .ToListAsync();
        }

        public async new Task<bool> ExistsAsync(Supplier entity)
            => await DbContext.Suppliers.AnyAsync(x => x.SupplierId == entity.SupplierId);
    }
}
