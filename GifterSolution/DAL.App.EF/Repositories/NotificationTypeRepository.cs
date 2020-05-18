using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using com.mubbly.gifterapp.DAL.Base.EF.Repositories;
using com.mubbly.gifterapp.DAL.Base.Mappers;
using Microsoft.EntityFrameworkCore;
using DomainApp = Domain.App;
using DALAppDTO = DAL.App.DTO;
using DomainAppIdentity = Domain.App.Identity;

namespace DAL.App.EF.Repositories
{
    public class NotificationTypeRepository :
        EFBaseRepository<AppDbContext, DomainAppIdentity.AppUser, DomainApp.NotificationType, DALAppDTO.NotificationTypeDAL
        >,
        INotificationTypeRepository
    {
        public NotificationTypeRepository(AppDbContext dbContext) :
            base(dbContext, new DALMapper<DomainApp.NotificationType, DALAppDTO.NotificationTypeDAL>())
        {
        }
        
        // public override async Task<IEnumerable<DALAppDTO.NotificationType>> GetAllAsync(object? userId = null, bool noTracking = true)
        // {
        //     var query = PrepareQuery(userId, noTracking);
        //     query = query
        //         .Include(l => l.NotificationTypeValue)
        //         .Include(l => l.Comment);
        //     
        //     var domainEntities = await query.ToListAsync();
        //     var result = domainEntities.Select(e => Mapper.Map(e));
        //     return result;
        // }

        // // TODO: User stuff
        //
        // public async Task<IEnumerable<NotificationType>> AllAsync(Guid? userId = null)
        // {
        //     return await base.AllAsync();
        // }
        //
        // public async Task<NotificationType> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Where(nt => nt.Id == id)
        //         .AsQueryable();
        //     return await query.FirstOrDefaultAsync();
        // }
        //
        // public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        // {
        //     return await RepoDbSet.AnyAsync(nt => nt.Id == id);
        // }
        //
        // public async Task DeleteAsync(Guid id, Guid? userId = null)
        // {
        //     var notificationType = await FirstOrDefaultAsync(id, userId);
        //     base.Remove(notificationType);
        // }
        //
        // public async Task<IEnumerable<NotificationTypeDTO>> DTOAllAsync(Guid? userId = null)
        // {
        //     var query = RepoDbSet.AsQueryable();
        //     return await query
        //         .Select(nt => new NotificationTypeDTO
        //         {
        //             Id = nt.Id,
        //             NotificationTypeValue = nt.NotificationTypeValue,
        //             Comment = nt.Comment,
        //             NotificationsCount = nt.Notifications.Count
        //         }).ToListAsync();
        // }
        //
        // public async Task<NotificationTypeDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Where(nt=> nt.Id == id)
        //         .AsQueryable();
        //     return await query
        //         .Select(nt => new NotificationTypeDTO 
        //         {
        //             Id = nt.Id,
        //             NotificationTypeValue = nt.NotificationTypeValue,
        //             Comment = nt.Comment,
        //             NotificationsCount = nt.Notifications.Count
        //         }).FirstOrDefaultAsync();
        // }
    }
}