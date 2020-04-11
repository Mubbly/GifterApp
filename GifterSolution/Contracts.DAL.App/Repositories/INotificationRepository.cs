using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1;

namespace Contracts.DAL.App.Repositories
{
    public interface INotificationRepository : IBaseRepository<Notification>
    {
        Task<IEnumerable<Notification>> AllAsync(Guid? userId = null);
        Task<Notification> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);

        // DTO methods
        Task<IEnumerable<NotificationDTO>> DTOAllAsync(Guid? userId = null);
        Task<NotificationDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);     
    }
}