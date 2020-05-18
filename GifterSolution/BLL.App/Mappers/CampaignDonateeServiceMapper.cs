using Contracts.BLL.App.Mappers;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class CampaignDonateeServiceMapper : BLLMapper<DALAppDTO.CampaignDonateeDAL, BLLAppDTO.CampaignDonateeBLL>,
        ICampaignDonateeServiceMapper
    {
    }
}