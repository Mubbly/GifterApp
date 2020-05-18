using com.mubbly.gifterapp.BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class DonateeServiceMapper : BLLMapper<DALAppDTO.DonateeDAL, BLLAppDTO.DonateeBLL>, IDonateeServiceMapper
    {
        public BLLAppDTO.CampaignDonateeBLL MapCampaignDonateeToBLL(DALAppDTO.CampaignDonateeDAL inObject)
        {
            return Mapper.Map<BLLAppDTO.CampaignDonateeBLL>(inObject);
        }

        public DALAppDTO.CampaignDonateeDAL MapCampaignDonateeToDAL(BLLAppDTO.CampaignDonateeBLL inObject)
        {
            return Mapper.Map<DALAppDTO.CampaignDonateeDAL>(inObject);
        }
    }
}