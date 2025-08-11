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
public interface IBusinessUserRole
{
    Task<IEnumerable<UserRole>> GetAllAsync();
    Task<UserRole?> GetByIdAsync(decimal id);
    Task<bool> SaveAsync(UserRole userRole);
    Task<bool> DeleteAsync(decimal id);
    Task<IEnumerable<UserRoleViewModel>> FilterAsync(Expression<Func<UserRole, bool>> predicate);
}

public class BusinessUserRole(IRepositoryUserRole repo) : IBusinessUserRole
{
    public Task<IEnumerable<UserRole>> GetAllAsync() => repo.ReadAsync();
    public Task<UserRole?> GetByIdAsync(decimal id) => repo.FindAsync(id);
    public async Task<bool> SaveAsync(UserRole ur)
        => ur.Id.HasValue && ur.Id.Value > 0 ? await repo.UpdateAsync(ur) : await repo.CreateAsync(ur);
    public async Task<bool> DeleteAsync(decimal id)
    {
        var found = await repo.FindAsync(id);
        return found != null && await repo.DeleteAsync(found);
    }
    public Task<IEnumerable<UserRoleViewModel>> FilterAsync(Expression<Func<UserRole, bool>> p)
        => repo.FilterAsync(p);
}
