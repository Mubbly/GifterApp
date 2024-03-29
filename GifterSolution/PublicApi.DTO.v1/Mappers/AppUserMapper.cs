﻿using com.mubbly.gifterapp.Domain.Base;
using PublicApi.DTO.v1.Identity;
using BLLAppDTO=BLL.App.DTO.Identity;

namespace PublicApi.DTO.v1.Mappers
{
    public class AppUserMapper : BaseMapper<Domain.App.Identity.AppUser, AppUserDTO>
    {
    }
}