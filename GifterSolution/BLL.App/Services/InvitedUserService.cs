﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mappers;
using com.mubbly.gifterapp.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Services
{
    public class InvitedUserService : BaseEntityService<IAppUnitOfWork,
            IInvitedUserRepository, IInvitedUserServiceMapper, DALAppDTO.InvitedUserDAL, BLLAppDTO.InvitedUserBLL>,
        IInvitedUserService
    {
        public InvitedUserService(IAppUnitOfWork uow) : base(uow, uow.InvitedUsers, new InvitedUserServiceMapper())
        {
        }
        
        public async Task<IEnumerable<BLLAppDTO.InvitedUserBLL>> GetAllPersonalAsync(Guid userId, bool noTracking = true)
        {
            var personalInvitedUsers = await Repository.GetAllPersonalAsync(userId, noTracking);
            return personalInvitedUsers.Select(e => Mapper.Map(e));
        }
    }
}