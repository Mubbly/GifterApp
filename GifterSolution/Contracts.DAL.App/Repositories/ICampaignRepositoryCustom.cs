using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ICampaignRepositoryCustom : ICampaignRepositoryCustom<DALAppDTO.Campaign>
    {
    }

    public interface ICampaignRepositoryCustom<TCampaign>
    {
    }
}