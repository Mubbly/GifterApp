using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class NotificationTypeRepository : BaseRepository<NotificationType>, INotificationTypeRepository
    {
        public NotificationTypeRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}