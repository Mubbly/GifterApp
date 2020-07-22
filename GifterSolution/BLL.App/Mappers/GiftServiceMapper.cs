using Contracts.BLL.App.Mappers;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class GiftServiceMapper : BLLMapper<DALAppDTO.GiftDAL, BLLAppDTO.GiftBLL>, IGiftServiceMapper
    {
        public DALAppDTO.ReservedGiftDAL MapReservedGiftFullToDAL(BLLAppDTO.ReservedGiftFullBLL inObject)
        {
            return Mapper.Map<DALAppDTO.ReservedGiftDAL>(inObject);
        }
        
        public BLLAppDTO.ReservedGiftResponseBLL MapReservedGiftFullToResponse(BLLAppDTO.ReservedGiftFullBLL inObject)
        {
            return Mapper.Map<BLLAppDTO.ReservedGiftResponseBLL>(inObject);
        }
        
        public DALAppDTO.ArchivedGiftDAL MapArchivedGiftFullToDAL(BLLAppDTO.ArchivedGiftFullBLL inObject)
        {
            return Mapper.Map<DALAppDTO.ArchivedGiftDAL>(inObject);
        }
        
        public BLLAppDTO.ArchivedGiftResponseBLL MapArchivedGiftFullToResponse(BLLAppDTO.ArchivedGiftFullBLL inObject) // TODO: Fix
        {
            return Mapper.Map<BLLAppDTO.ArchivedGiftResponseBLL>(inObject);
        }

        public BLLAppDTO.ArchivedGiftResponseBLL MapArchivedGiftDALToResponse(DALAppDTO.ArchivedGiftDAL inObject)
        {
            return Mapper.Map<BLLAppDTO.ArchivedGiftResponseBLL>(inObject);
        }
    }
}