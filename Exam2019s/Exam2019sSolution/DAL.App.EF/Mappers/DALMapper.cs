using AutoMapper;
using com.mubbly.gifterapp.DAL.Base.Mappers;
using DAL.App.DTO;
using DAL.App.DTO.Identity;
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
            MapperConfigurationExpression.CreateMap<AppUser, AppUserDAL>();
            MapperConfigurationExpression.CreateMap<AppUserDAL, AppUser>();

            MapperConfigurationExpression.CreateMap<Example, ExampleDAL>();
            MapperConfigurationExpression.CreateMap<ExampleDAL, Example>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}