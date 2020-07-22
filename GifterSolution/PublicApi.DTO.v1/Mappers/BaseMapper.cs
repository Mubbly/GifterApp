using AutoMapper;
using BLL.App.DTO;
using BLL.App.DTO.Identity;
using DAL.App.DTO;
using DAL.App.DTO.Identity;

namespace PublicApi.DTO.v1.Mappers
{
    public abstract class BaseMapper<TLeftObject, TRightObject> : com.mubbly.gifterapp.DAL.Base.Mappers.BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public BaseMapper() : base()
        {
            MapperConfigurationExpression.CreateMap<FriendshipResponseBLL, FriendshipResponseDTO>();
            MapperConfigurationExpression.CreateMap<ReservedGiftDTO, ReservedGiftBLL>();
            MapperConfigurationExpression.CreateMap<ArchivedGiftDTO, ArchivedGiftBLL>();
            MapperConfigurationExpression.CreateMap<UserNotificationBLL, UserNotificationDTO>();
            MapperConfigurationExpression.CreateMap<UserNotificationEditDTO, UserNotificationBLL>();
            // MapperConfigurationExpression.CreateMap<ReservedGiftResponseBLL, ReservedGiftResponseDTO>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}