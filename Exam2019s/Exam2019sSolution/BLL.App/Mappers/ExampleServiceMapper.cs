using BLL.App.DTO;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class ExampleServiceMapper : BLLMapper<ExampleDAL, ExampleBLL>, IExampleServiceMapper
    {
        
    }
}