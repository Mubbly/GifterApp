using BLLAppDTO = BLL.App.DTO;

namespace PublicApi.DTO.v1.Mappers
{
    public class GiftMapper : BaseMapper<BLLAppDTO.GiftBLL, GiftDTO>
    {
        public BLLAppDTO.ReservedGiftBLL MapReservedGiftDTOToBLL(ReservedGiftDTO inObject)
        {
            return Mapper.Map<BLLAppDTO.ReservedGiftBLL>(inObject);
        }
        
        public BLLAppDTO.ArchivedGiftBLL MapArchivedGiftDTOToBLL(ArchivedGiftDTO inObject)
        {
            return Mapper.Map<BLLAppDTO.ArchivedGiftBLL>(inObject);
        }
        // public ReservedGiftResponseDTO MapReservedGiftResponseBLLToResponseDTO(BLLAppDTO.ReservedGiftResponseBLL inObject)
        // {
        //     return Mapper.Map<ReservedGiftResponseDTO>(inObject);
        // }
    }
}