using com.mubbly.gifterapp.Contracts.BLL.Base.Mappers;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Mappers
{
    public interface IUserCampaignServiceMapper : IBaseMapper<DALAppDTO.UserCampaignDAL, BLLAppDTO.UserCampaignBLL>
    {
    }
}