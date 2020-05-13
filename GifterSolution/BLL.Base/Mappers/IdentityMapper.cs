using DALBaseMappers = DAL.Base.Mappers;

namespace BLL.Base.Mappers
{
    public class IdentityMapper<TLeftObject, TRightObject> : DALBaseMappers.IdentityMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
    }
}