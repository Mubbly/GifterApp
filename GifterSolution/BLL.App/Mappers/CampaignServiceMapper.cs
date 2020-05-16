using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using Domain.App;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class CampaignServiceMapper : BLLMapper<DALAppDTO.CampaignDAL, BLLAppDTO.CampaignBLL>, ICampaignServiceMapper
    {
        public CampaignServiceMapper() :base()
        {
        }

        public BLLAppDTO.UserCampaignBLL MapUserCampaignToBLL(DALAppDTO.UserCampaignDAL inObject)
        {
            return Mapper.Map<BLLAppDTO.UserCampaignBLL>(inObject);
        }

        public DALAppDTO.UserCampaignDAL MapUserCampaignToDAL(BLLAppDTO.UserCampaignBLL inObject)
        {
            return Mapper.Map<DALAppDTO.UserCampaignDAL>(inObject);
        }
    }
}