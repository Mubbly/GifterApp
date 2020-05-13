namespace Contracts.BLL.Base.Mappers
{
    public interface IBaseMapper<TLeftObject, TRightObject> : DAL.Base.Mappers.IBaseMapper<TLeftObject, TRightObject>
        where TLeftObject : class?, new()
        where TRightObject : class?, new()
    {
    }
}