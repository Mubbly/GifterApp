using AutoMapper;
using BLL.App.DTO;
using BLL.App.DTO.Identity;
using com.mubbly.gifterapp.BLL.Base.Mappers;
using DAL.App.DTO;
using DAL.App.DTO.Identity;

namespace BLL.App.Mappers
{
    public class BLLMapper<TLeftObject, TRightObject> : BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public BLLMapper() : base()
        {
            MapperConfigurationExpression.CreateMap<AppUserBLL, AppUserDAL>();
            MapperConfigurationExpression.CreateMap<AppUserDAL, AppUserBLL>();
            
            MapperConfigurationExpression.CreateMap<ExampleBLL, ExampleDAL>();
            MapperConfigurationExpression.CreateMap<ExampleDAL, ExampleBLL>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}