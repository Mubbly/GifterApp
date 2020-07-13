using Contracts.BLL.App.Mappers;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class ArchivedGiftServiceMapper : BLLMapper<DALAppDTO.ArchivedGiftDAL, BLLAppDTO.ArchivedGiftFullBLL>,
        IArchivedGiftServiceMapper
    {
        public BLLAppDTO.GiftBLL MapGiftToBLL(DALAppDTO.GiftDAL inObject)
        {
            return Mapper.Map<BLLAppDTO.GiftBLL>(inObject);
        }
    }
}