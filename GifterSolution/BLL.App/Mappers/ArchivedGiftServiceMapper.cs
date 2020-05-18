using Contracts.BLL.App.Mappers;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class ArchivedGiftServiceMapper : BLLMapper<DALAppDTO.ArchivedGiftDAL, BLLAppDTO.ArchivedGiftBLL>,
        IArchivedGiftServiceMapper
    {
    }
}