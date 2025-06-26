using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IUserRoleDropdownService
    {
        List<UserRoleDropdown> GetDropDownData();
    }
}
