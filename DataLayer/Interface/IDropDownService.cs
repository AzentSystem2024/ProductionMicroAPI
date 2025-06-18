using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IDropDownService
    {
        List<DropDown> GetDropDownData(string vName);

    }
}
