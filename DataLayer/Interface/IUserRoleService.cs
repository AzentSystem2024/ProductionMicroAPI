using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IUserRoleService
    {
        public UserRoleResponse GetAllUserRoles(int userId);
        public UserMenuResponse GetAllUsermainRoles(int intUserID);
        public int Insert(UserRole userrole, Int32 userID);
        public int Update(UserRole userrole, Int32 userID);
        public void DeleteUserRole(int Id, int userID);
        //public UserRole GetItems(int id);
        public UserRoleResponse GetUserRoleById(int userId, int roleId);
    }
}
