﻿using AutoMapper;
using BLL.App.DTO.Identity;
using PublicApi.DTO.v1.Identity;
using BLLAppDTO = BLL.App.DTO;

namespace PublicApi.DTO.v1.Mappers
{
    public class ProfileMapper : BaseMapper<BLLAppDTO.ProfileBLL, ProfileDTO>
    {
        public ProfileMapper() : base()
        {
            MapperConfigurationExpression.CreateMap<AppUserBLL, AppUserDTO>();
            MapperConfigurationExpression.CreateMap<AppUserDTO, AppUserBLL>();

            MapperConfigurationExpression.CreateMap<BLLAppDTO.WishlistBLL, WishlistDTO>();
            MapperConfigurationExpression.CreateMap<WishlistDTO, BLLAppDTO.WishlistBLL>();

            // MapperConfigurationExpression.CreateMap<BLLAppDTO.ProfileBLL, ProfileDTO>()
            //     .AfterMap((bll, dto) => Mapper.Map(bll.AppUser, dto.AppUser))
            //     .AfterMap((bll, dto) => Mapper.Map(bll.Wishlist, dto.Wishlist));

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}