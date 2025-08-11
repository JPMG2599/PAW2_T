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
    public interface IRepositoryNotification
    {
        Task<bool> UpsertAsync(Notification entity, bool isUpdating);
        Task<bool> CreateAsync(Notification entity);
        Task<bool> DeleteAsync(Notification entity);
        Task<IEnumerable<Notification>> ReadAsync();
        Task<Notification?> FindAsync(int id);
        Task<bool> UpdateAsync(Notification entity);
        Task<bool> UpdateManyAsync(IEnumerable<Notification> entities);
        Task<bool> ExistsAsync(Notification entity);
        Task<bool> CheckBeforeSavingAsync(Notification entity);
        Task<IEnumerable<NotificationViewModel>> FilterAsync(Expression<Func<Notification, bool>> predicate);
    }

    public class RepositoryNotification : RepositoryBase<Notification>, IRepositoryNotification
    {
        public async Task<bool> CheckBeforeSavingAsync(Notification entity)
        {
            var exists = await ExistsAsync(entity);
            return await UpsertAsync(entity, exists);
        }

        public async Task<IEnumerable<NotificationViewModel>> FilterAsync(Expression<Func<Notification, bool>> predicate)
        {
            return await DbContext.Notifications
                .Where(predicate)
                .Select(n => new NotificationViewModel
                {
                    Id = n.Id,
                    UserId = n.UserId,
                    Message = n.Message,
                    IsRead = n.IsRead,
                    CreatedAt = n.CreatedAt
                })
                .ToListAsync();
        }

        public async new Task<bool> ExistsAsync(Notification entity)
            => await DbContext.Notifications.AnyAsync(x => x.Id == entity.Id);
    }
}