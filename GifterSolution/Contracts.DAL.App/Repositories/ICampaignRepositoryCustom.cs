using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ICampaignRepositoryCustom : ICampaignRepositoryCustom<DALAppDTO.CampaignDAL>
    {
    }

    public interface ICampaignRepositoryCustom<TCampaign>
    {
    }
}