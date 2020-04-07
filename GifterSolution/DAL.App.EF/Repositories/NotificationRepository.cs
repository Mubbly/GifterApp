using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;

namespace DAL.App.EF.Repositories
{
    public class NotificationRepository : EFBaseRepository<Notification, AppDbContext>, INotificationRepository
    {
        public NotificationRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}