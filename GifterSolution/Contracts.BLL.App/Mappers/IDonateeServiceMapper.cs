using Contracts.BLL.Base.Mappers;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Mappers
{
    public interface IDonateeServiceMapper : IBaseMapper<DALAppDTO.DonateeDAL, BLLAppDTO.DonateeBLL>
    {
        BLLAppDTO.CampaignDonateeBLL MapCampaignDonateeToBLL(DALAppDTO.CampaignDonateeDAL inObject);
        DALAppDTO.CampaignDonateeDAL MapCampaignDonateeToDAL(BLLAppDTO.CampaignDonateeBLL inObject);
    }
}