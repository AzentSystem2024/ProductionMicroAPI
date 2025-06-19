using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IDropDownService
    {
        List<DropDown> GetDropDownData(string vName, int countryId = 0, int stateId = 0, int districtId = 0);
    }
}
