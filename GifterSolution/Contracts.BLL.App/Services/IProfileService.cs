﻿using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IProfileService : IBaseEntityService<BLLAppDTO.Profile>,
        IProfileRepositoryCustom<BLLAppDTO.Profile>
    {
    }
}