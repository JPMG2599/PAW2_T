using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PAW.Models.ViewModels;
using PAW.Models;

namespace PAW.Repositories;
public interface IRepositoryUserRole
{
    Task<bool> UpsertAsync(UserRole entity, bool isUpdating);
    Task<bool> CreateAsync(UserRole entity);
    Task<bool> DeleteAsync(UserRole entity);
    Task<IEnumerable<UserRole>> ReadAsync();
    Task<UserRole?> FindAsync(decimal id);               
    Task<bool> UpdateAsync(UserRole entity);
    Task<bool> UpdateManyAsync(IEnumerable<UserRole> entities);
    Task<bool> ExistsAsync(UserRole entity);
    Task<bool> CheckBeforeSavingAsync(UserRole entity);
    Task<IEnumerable<UserRoleViewModel>> FilterAsync(Expression<Func<UserRole, bool>> predicate);
}

public class RepositoryUserRole : RepositoryBase<UserRole>, IRepositoryUserRole
{
    public async Task<bool> CheckBeforeSavingAsync(UserRole entity)
    {
        var exists = await ExistsAsync(entity);
        return await UpsertAsync(entity, exists);
    }

    public async Task<UserRole?> FindAsync(decimal id)
        => await DbContext.UserRoles.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<UserRoleViewModel>> FilterAsync(Expression<Func<UserRole, bool>> predicate)
    {
        return await DbContext.UserRoles
            .Where(predicate)
            .Select(ur => new UserRoleViewModel
            {
                Id = ur.Id,
                RoldId = ur.RoldId,
                UserId = ur.UserId
            })
            .ToListAsync();
    }

    public async new Task<bool> ExistsAsync(UserRole entity)
        => await DbContext.UserRoles.AnyAsync(x => x.Id == entity.Id);
}