using Contracts.BLL.App.Mappers;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class GiftServiceMapper : BLLMapper<DALAppDTO.GiftDAL, BLLAppDTO.GiftBLL>, IGiftServiceMapper
    {
    }
}