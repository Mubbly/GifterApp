﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1;

namespace Contracts.DAL.App.Repositories
{
    public interface IUserNotificationRepository : IBaseRepository<UserNotification>
    {
        Task<IEnumerable<UserNotification>> AllAsync(Guid? userId = null);
        Task<UserNotification> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);

        // DTO methods
        Task<IEnumerable<UserNotificationDTO>> DTOAllAsync(Guid? userId = null);
        Task<UserNotificationDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);     
    }
}