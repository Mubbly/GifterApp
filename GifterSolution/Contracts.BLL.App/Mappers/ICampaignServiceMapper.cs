using Contracts.BLL.Base.Mappers;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Mappers
{
    public interface ICampaignServiceMapper : IBaseMapper<DALAppDTO.CampaignDAL, BLLAppDTO.CampaignBLL>
    {
        BLLAppDTO.UserCampaignBLL MapUserCampaignToBLL(DALAppDTO.UserCampaignDAL inObject);
        DALAppDTO.UserCampaignDAL MapUserCampaignToDAL(BLLAppDTO.UserCampaignBLL inObject);
    }
}