﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1;

namespace Contracts.DAL.App.Repositories
{
    public interface IActionTypeRepository : IBaseRepository<ActionType>
    {
        Task<IEnumerable<ActionType>> AllAsync(Guid? userId = null);
        Task<ActionType> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);

        // DTO methods
        Task<IEnumerable<ActionTypeDTO>> DTOAllAsync(Guid? userId = null);
        Task<ActionTypeDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);     
    }
}