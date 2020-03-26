using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class NotificationTypeRepository : EFBaseRepository<NotificationType, AppDbContext>, INotificationTypeRepository
    {
        public NotificationTypeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}