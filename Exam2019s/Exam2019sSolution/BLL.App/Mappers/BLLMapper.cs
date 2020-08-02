using AutoMapper;
using com.mubbly.gifterapp.BLL.Base.Mappers;
namespace BLL.App.Mappers
{
    public class BLLMapper<TLeftObject, TRightObject> : BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public BLLMapper() : base()
        {
            // Example:
            // MapperConfigurationExpression.CreateMap<AppUserBLL, AppUserDAL>();
            // MapperConfigurationExpression.CreateMap<AppUserDAL, AppUserBLL>();
            // MapperConfigurationExpression.CreateMap<InvitedUserBLL, InvitedUserDAL>();
            // MapperConfigurationExpression.CreateMap<InvitedUserDAL, InvitedUserBLL>();
            // MapperConfigurationExpression.CreateMap<FriendshipBLL, FriendshipDAL>();
            // MapperConfigurationExpression.CreateMap<FriendshipDAL, FriendshipBLL>();
            // MapperConfigurationExpression.CreateMap<FriendshipDAL, FriendshipResponseBLL>();
            // MapperConfigurationExpression.CreateMap<PrivateMessageBLL, PrivateMessageDAL>();
            // MapperConfigurationExpression.CreateMap<PrivateMessageDAL, PrivateMessageBLL>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}