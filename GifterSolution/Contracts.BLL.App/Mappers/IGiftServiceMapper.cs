using com.mubbly.gifterapp.Contracts.BLL.Base.Mappers;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Mappers
{
    public interface IGiftServiceMapper : IBaseMapper<DALAppDTO.GiftDAL, BLLAppDTO.GiftBLL>
    {
        DALAppDTO.ReservedGiftDAL MapReservedGiftFullToDAL(BLLAppDTO.ReservedGiftFullBLL inObject);
        BLLAppDTO.ReservedGiftResponseBLL MapReservedGiftFullToResponse(BLLAppDTO.ReservedGiftFullBLL inObject);
        DALAppDTO.ArchivedGiftDAL MapArchivedGiftFullToDAL(BLLAppDTO.ArchivedGiftFullBLL inObject);
        BLLAppDTO.ArchivedGiftResponseBLL MapArchivedGiftFullToResponse(BLLAppDTO.ArchivedGiftFullBLL inObject);
        BLLAppDTO.ArchivedGiftResponseBLL MapArchivedGiftDALToResponse(DALAppDTO.ArchivedGiftDAL inObject);
    }
}