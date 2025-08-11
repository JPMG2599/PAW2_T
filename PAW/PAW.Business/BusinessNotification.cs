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
public interface IBusinessNotification
{
    Task<IEnumerable<Notification>> GetAllAsync();
    Task<Notification?> GetByIdAsync(int id);
    Task<bool> SaveAsync(Notification notification);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<NotificationViewModel>> FilterAsync(Expression<Func<Notification, bool>> predicate);
}

public class BusinessNotification(IRepositoryNotification repo) : IBusinessNotification
{
    public Task<IEnumerable<Notification>> GetAllAsync() => repo.ReadAsync();
    public Task<Notification?> GetByIdAsync(int id) => repo.FindAsync(id);

    public async Task<bool> SaveAsync(Notification n)
        => n.Id > 0 ? await repo.UpdateAsync(n) : await repo.CreateAsync(n);

    public async Task<bool> DeleteAsync(int id)
    {
        var found = await repo.FindAsync(id);
        return found != null && await repo.DeleteAsync(found);
    }

    public Task<IEnumerable<NotificationViewModel>> FilterAsync(Expression<Func<Notification, bool>> p)
        => repo.FilterAsync(p);
}