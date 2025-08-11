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
public interface IBusinessUser
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<bool> SaveAsync(User user);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<UserViewModel>> FilterAsync(Expression<Func<User, bool>> predicate);
}

public class BusinessUser(IRepositoryUser repo) : IBusinessUser
{
    public Task<IEnumerable<User>> GetAllAsync() => repo.ReadAsync();
    public Task<User?> GetByIdAsync(int id) => repo.FindAsync(id);
    public async Task<bool> SaveAsync(User u)
        => u.UserId > 0 ? await repo.UpdateAsync(u) : await repo.CreateAsync(u);
    public async Task<bool> DeleteAsync(int id)
    {
        var found = await repo.FindAsync(id);
        return found != null && await repo.DeleteAsync(found);
    }
    public Task<IEnumerable<UserViewModel>> FilterAsync(Expression<Func<User, bool>> p)
        => repo.FilterAsync(p);
}
