using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IUserSecurityService
    {
        public UserSecurityResponse GetAllUserSecurity(Int32 intUserID);
        public ChangePasswordResponse UpdateUserPassword(ChangePassword changePassword, Int32 userID);

    }
}
