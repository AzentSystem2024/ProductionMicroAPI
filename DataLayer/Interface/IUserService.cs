using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IUserService
    {
        UserResponse Insert(User user);
        UserResponse Update(UserUpdate user);
        UserSelectResponse GetUserById(int id);
        UserListResponse GetLogList(int? id = null);

        UserResponse DeleteUserData(int id);


    }
}
