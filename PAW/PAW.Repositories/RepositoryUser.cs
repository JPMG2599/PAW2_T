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
    public interface IRepositoryUser
    {
        Task<bool> UpsertAsync(User entity, bool isUpdating);
        Task<bool> CreateAsync(User entity);
        Task<bool> DeleteAsync(User entity);
        Task<IEnumerable<User>> ReadAsync();
        Task<User?> FindAsync(int id);
        Task<bool> UpdateAsync(User entity);
        Task<bool> UpdateManyAsync(IEnumerable<User> entities);
        Task<bool> ExistsAsync(User entity);
        Task<bool> CheckBeforeSavingAsync(User entity);
        Task<IEnumerable<UserViewModel>> FilterAsync(Expression<Func<User, bool>> predicate);
    }

    public class RepositoryUser : RepositoryBase<User>, IRepositoryUser
    {
        public async Task<bool> CheckBeforeSavingAsync(User entity)
        {
            var exists = await ExistsAsync(entity);
            return await UpsertAsync(entity, exists);
        }

        public async Task<IEnumerable<UserViewModel>> FilterAsync(Expression<Func<User, bool>> predicate)
        {
            return await DbContext.Users
                .Where(predicate)
                .Select(u => new UserViewModel
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    Email = u.Email,
                    PasswordHash = u.PasswordHash,
                    CreatedAt = u.CreatedAt,
                    IsActive = u.IsActive,
                    LastModified = u.LastModified,
                    ModifiedBy = u.ModifiedBy
                })
                .ToListAsync();
        }

        public async new Task<bool> ExistsAsync(User entity)
            => await DbContext.Users.AnyAsync(x => x.UserId == entity.UserId);
    }
}
