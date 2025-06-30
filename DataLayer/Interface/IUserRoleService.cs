using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IUserRoleService
    {
        public UserRoleResponse GetAllUserRoles(int userId);
        public UserMenuResponse GetAllUsermainRoles(int intUserID);
        public int Insert(UserRole userrole, Int32 userID);
        public int Update(UserRole userrole, Int32 userID);
         UserMenuResponse DeleteUserRole(int Id);
        public UserRole GetItems(int id);
    }
}
