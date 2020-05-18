using Contracts.BLL.App.Mappers;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class StatusServiceMapper : BLLMapper<DALAppDTO.StatusDAL, BLLAppDTO.StatusBLL>, IStatusServiceMapper
    {
    }
}