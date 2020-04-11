using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;

namespace DAL.App.EF.Repositories
{
    public class NotificationTypeRepository : EFBaseRepository<NotificationType, AppDbContext>, INotificationTypeRepository
    {
        public NotificationTypeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        // TODO: User stuff

        public async Task<IEnumerable<NotificationType>> AllAsync(Guid? userId = null)
        {
            return await base.AllAsync();
        }

        public async Task<NotificationType> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Where(nt => nt.Id == id)
                .AsQueryable();
            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            return await RepoDbSet.AnyAsync(nt => nt.Id == id);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var notificationType = await FirstOrDefaultAsync(id, userId);
            base.Remove(notificationType);
        }

        public async Task<IEnumerable<NotificationTypeDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            return await query
                .Select(nt => new NotificationTypeDTO
                {
                    Id = nt.Id,
                    NotificationTypeValue = nt.NotificationTypeValue,
                    Comment = nt.Comment,
                    NotificationsCount = nt.Notifications.Count
                }).ToListAsync();
        }

        public async Task<NotificationTypeDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Where(nt=> nt.Id == id)
                .AsQueryable();
            return await query
                .Select(nt => new NotificationTypeDTO 
                {
                    Id = nt.Id,
                    NotificationTypeValue = nt.NotificationTypeValue,
                    Comment = nt.Comment,
                    NotificationsCount = nt.Notifications.Count
                }).FirstOrDefaultAsync();
        }
    }
}