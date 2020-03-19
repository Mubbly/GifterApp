using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class UserNotificationRepository : BaseRepository<UserNotification>, IUserNotificationRepository
    {
        public UserNotificationRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}