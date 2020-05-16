using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IUserCampaignRepositoryCustom : IUserCampaignRepositoryCustom<DALAppDTO.UserCampaignDAL>
    {
    }

    public interface IUserCampaignRepositoryCustom<TUserCampaign>
    {
    }
}