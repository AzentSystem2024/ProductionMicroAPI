using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IUserMenuPermissionService
    {
        UserMenuPermission GetUserMenuPermissions(int userId, int menuId);
    }
}
