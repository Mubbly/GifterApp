﻿using BLLAppDTO = BLL.App.DTO;

namespace PublicApi.DTO.v1.Mappers
{
    public class FriendshipMapper : BaseMapper<BLLAppDTO.FriendshipBLL, FriendshipDTO>
    {
        public FriendshipResponseDTO MapFriendshipResponseToDTO(BLLAppDTO.FriendshipResponseBLL inObject)
        {
            return Mapper.Map<FriendshipResponseDTO>(inObject);
        }
    }
}