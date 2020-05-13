using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ICampaignDonateeRepositoryCustom : ICampaignDonateeRepositoryCustom<DALAppDTO.CampaignDonatee>
    {
    }

    public interface ICampaignDonateeRepositoryCustom<TCampaignDonatee>
    {
    }
}