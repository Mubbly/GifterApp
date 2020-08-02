using BLL.App.DTO;

namespace PublicApi.DTO.v1.Mappers
{
    public class ExampleMapper : BaseMapper<ExampleBLL, ExampleDTO>
    {
        // Example:
        // public BLLAppDTO.AnotherExampleBLL MapAnotherExampleDTOToBLL(AnotherExampleDTO inObject)
        // {
        //     return Mapper.Map<BLLAppDTO.AnotherExampleBLL>(inObject);
        // }
    }
}