using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IUserCampaignRepositoryCustom : IUserCampaignRepositoryCustom<DALAppDTO.UserCampaign>
    {
    }

    public interface IUserCampaignRepositoryCustom<TUserCampaign>
    {
    }
}