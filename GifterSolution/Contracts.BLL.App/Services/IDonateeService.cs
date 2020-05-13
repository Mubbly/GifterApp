﻿using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IDonateeService : IBaseEntityService<BLLAppDTO.Donatee>,
        IDonateeRepositoryCustom<BLLAppDTO.Donatee>
    {
    }
}