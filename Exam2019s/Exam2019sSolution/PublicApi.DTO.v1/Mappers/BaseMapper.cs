using AutoMapper;
using BLL.App.DTO;
using BLL.App.DTO.Identity;
using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1.Mappers
{
    public abstract class BaseMapper<TLeftObject, TRightObject> : com.mubbly.gifterapp.DAL.Base.Mappers.BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public BaseMapper() : base()
        {
            // Example:
            // MapperConfigurationExpression.CreateMap<AnotherExampleBLL, AnotherExampleDTO>();
            
            MapperConfigurationExpression.CreateMap<AppUserBLL, AppUserDTO>();
            MapperConfigurationExpression.CreateMap<AppUserDTO, AppUserBLL>();
            
            MapperConfigurationExpression.CreateMap<ExampleBLL, ExampleDTO>();
            MapperConfigurationExpression.CreateMap<ExampleDTO, ExampleBLL>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}