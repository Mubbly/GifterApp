using AutoMapper;
using com.mubbly.gifterapp.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class DALMapper<TLeftObject, TRightObject> : BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public DALMapper() : base()
        { 
            // Example:
            // MapperConfigurationExpression.CreateMap<AppUser, AppUserDAL>();
            // MapperConfigurationExpression.CreateMap<AppUserDAL, AppUser>();
            // MapperConfigurationExpression.CreateMap<InvitedUser, InvitedUserDAL>();
            // MapperConfigurationExpression.CreateMap<InvitedUserDAL, InvitedUser>();
            // MapperConfigurationExpression.CreateMap<Friendship, FriendshipDAL>();
            // MapperConfigurationExpression.CreateMap<FriendshipDAL, Friendship>();
            // MapperConfigurationExpression.CreateMap<PrivateMessage, PrivateMessageDAL>();
            // MapperConfigurationExpression.CreateMap<PrivateMessageDAL, PrivateMessage>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}