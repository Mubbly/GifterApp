using com.mubbly.gifterapp.DAL.Base.EF.Repositories;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DomainApp = Domain.App;
using DALAppDTO = DAL.App.DTO;
using DomainAppIdentity = Domain.App.Identity;

namespace DAL.App.EF.Repositories
{
    public class NotificationRepository :
        EFBaseRepository<AppDbContext, DomainAppIdentity.AppUser, DomainApp.Notification, DALAppDTO.NotificationDAL>,
        INotificationRepository
    {
        public NotificationRepository(AppDbContext dbContext) :
            base(dbContext, new DALMapper<DomainApp.Notification, DALAppDTO.NotificationDAL>())
        {
        }

        // // TODO: User stuff 
        //
        // public async Task<IEnumerable<Notification>> AllAsync(Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Include(n => n.NotificationType)
        //         .AsQueryable();
        //     return await query.ToListAsync();
        // }
        //
        // public async Task<Notification> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Include(n => n.NotificationType)
        //         .Where(n => n.Id == id)
        //         .AsQueryable();
        //     return await query.FirstOrDefaultAsync();
        // }
        //
        // public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        // {
        //     return await RepoDbSet.AnyAsync(n => n.Id == id);
        // }
        //
        // public async Task DeleteAsync(Guid id, Guid? userId = null)
        // {
        //     var notification = await FirstOrDefaultAsync(id, userId);
        //     base.Remove(notification);
        // }
        //
        // public async Task<IEnumerable<NotificationDTO>> DTOAllAsync(Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Include(n => n.NotificationType)
        //         .AsQueryable();
        //     return await query
        //         .Select(n => new NotificationDTO() 
        //         {
        //             Id = n.Id,
        //             NotificationValue = n.NotificationValue,
        //             Comment = n.Comment,
        //             NotificationTypeId = n.NotificationTypeId,
        //             UserNotificationsCount = n.UserNotifications.Count
        //         }).ToListAsync();
        // }
        //
        // public async Task<NotificationDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Include(n => n.NotificationType)
        //         .Where(n => n.Id == id)
        //         .AsQueryable();
        //     return await query
        //         .Select(n => new NotificationDTO() 
        //         {
        //             Id = n.Id,
        //             NotificationValue = n.NotificationValue,
        //             Comment = n.Comment,
        //             NotificationTypeId = n.NotificationTypeId,
        //             UserNotificationsCount = n.UserNotifications.Count
        //         }).FirstOrDefaultAsync();
        // }
    }
}