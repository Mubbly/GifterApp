﻿using Contracts.BLL.App.Mappers;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class PrivateMessageServiceMapper : BLLMapper<DALAppDTO.PrivateMessageDAL, BLLAppDTO.PrivateMessageBLL>,
        IPrivateMessageServiceMapper
    {
    }
}