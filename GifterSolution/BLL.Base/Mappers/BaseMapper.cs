using DALBaseMappers = DAL.Base.Mappers;

namespace BLL.Base.Mappers
{
    /// <summary>
    ///     Maps using Automapper. No mapper configuration. Property types and names have to match.
    /// </summary>
    /// <typeparam name="TLeftObject"></typeparam>
    /// <typeparam name="TRightObject"></typeparam>
    public class BaseMapper<TLeftObject, TRightObject> : DALBaseMappers.BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
    }
}