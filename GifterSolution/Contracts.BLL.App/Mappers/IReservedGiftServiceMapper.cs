using com.mubbly.gifterapp.Contracts.BLL.Base.Mappers;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Mappers
{
    public interface IReservedGiftServiceMapper : IBaseMapper<DALAppDTO.ReservedGiftDAL, BLLAppDTO.ReservedGiftFullBLL>
    {
        DALAppDTO.ArchivedGiftDAL MapReservedGiftToArchivedGift(BLLAppDTO.ReservedGiftFullBLL inObject);
        BLLAppDTO.ReservedGiftResponseBLL MapReservedGiftDALToResponseBLL(DALAppDTO.ReservedGiftDAL inObject);
    }
}