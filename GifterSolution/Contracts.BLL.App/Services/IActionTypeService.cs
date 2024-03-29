﻿using com.mubbly.gifterapp.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IActionTypeService : IBaseEntityService<BLLAppDTO.ActionTypeBLL>,
        IActionTypeRepositoryCustom<BLLAppDTO.ActionTypeBLL>
    {
    }
}