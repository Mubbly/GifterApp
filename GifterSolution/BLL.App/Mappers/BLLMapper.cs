using AutoMapper;
using BLL.App.DTO;
using BLL.Base.Mappers;
using DAL.App.DTO;
using Domain.App;

namespace BLL.App.Mappers
{
    public class BLLMapper<TLeftObject, TRightObject> : BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public BLLMapper() : base()
        {
            MapperConfigurationExpression.CreateMap<ActionTypeDAL, ActionTypeBLL>();
            MapperConfigurationExpression.CreateMap<ActionTypeBLL, ActionTypeDAL>();
            
            MapperConfigurationExpression.CreateMap<CampaignBLL, CampaignDAL>();
            MapperConfigurationExpression.CreateMap<CampaignDAL, CampaignBLL>();
            MapperConfigurationExpression.CreateMap<UserCampaignDAL, UserCampaignBLL>();
            MapperConfigurationExpression.CreateMap<UserCampaignBLL, UserCampaignDAL>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}