using Contracts.BLL.App.Mappers;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class ReservedGiftServiceMapper : BLLMapper<DALAppDTO.ReservedGiftDAL, BLLAppDTO.ReservedGiftFullBLL>,
        IReservedGiftServiceMapper
    {
        public DALAppDTO.ArchivedGiftDAL MapReservedGiftToArchivedGift(BLLAppDTO.ReservedGiftFullBLL inObject)
        {
            return Mapper.Map<DALAppDTO.ArchivedGiftDAL>(inObject);
        }
        
        public BLLAppDTO.ReservedGiftResponseBLL MapReservedGiftDALToResponseBLL(DALAppDTO.ReservedGiftDAL inObject)
        {
            return Mapper.Map<BLLAppDTO.ReservedGiftResponseBLL>(inObject);
        }
    }
}