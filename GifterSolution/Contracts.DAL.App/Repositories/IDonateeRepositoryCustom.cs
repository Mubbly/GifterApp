using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IDonateeRepositoryCustom : IDonateeRepositoryCustom<DALAppDTO.Donatee>
    {
    }

    public interface IDonateeRepositoryCustom<TDonatee>
    {
    }
}