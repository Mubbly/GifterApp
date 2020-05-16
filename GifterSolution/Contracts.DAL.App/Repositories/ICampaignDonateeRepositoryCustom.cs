using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ICampaignDonateeRepositoryCustom : ICampaignDonateeRepositoryCustom<DALAppDTO.CampaignDonateeDAL>
    {
    }

    public interface ICampaignDonateeRepositoryCustom<TCampaignDonatee>
    {
    }
}