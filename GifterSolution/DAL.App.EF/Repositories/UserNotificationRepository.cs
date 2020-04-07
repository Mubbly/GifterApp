using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;

namespace DAL.App.EF.Repositories
{
    public class UserNotificationRepository : EFBaseRepository<UserNotification, AppDbContext>, IUserNotificationRepository
    {
        public UserNotificationRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}