using AutoMapper;
using BLL.App.DTO;
using DAL.App.DTO;
using DAL.App.DTO.Identity;
using DAL.Base.Mappers;
using Domain.App;
using Domain.App.Identity;

namespace DAL.App.EF.Mappers
{
    public class DALMapper<TLeftObject, TRightObject> : BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public DALMapper() : base()
        { 
            // add more mappings. TODO: Why are manual hardcoded mappings necessary?
            MapperConfigurationExpression.CreateMap<AppUser, AppUserDAL>();
            MapperConfigurationExpression.CreateMap<AppUserDAL, AppUser>();

            MapperConfigurationExpression.CreateMap<ActionType, ActionTypeDAL>();
            MapperConfigurationExpression.CreateMap<ActionTypeDAL, ActionType>();
            
            MapperConfigurationExpression.CreateMap<CampaignDAL, Campaign>();
            MapperConfigurationExpression.CreateMap<Campaign, CampaignDAL>();
            MapperConfigurationExpression.CreateMap<UserCampaignDAL, UserCampaign>();
            MapperConfigurationExpression.CreateMap<UserCampaign, UserCampaignDAL>();

            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}