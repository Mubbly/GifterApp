﻿using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class ProfileServiceMapper : BaseMapper<DALAppDTO.Profile, BLLAppDTO.Profile>, IProfileServiceMapper
    {
    }
}