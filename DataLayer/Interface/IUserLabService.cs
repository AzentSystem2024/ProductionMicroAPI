using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IUserLabService
    {
        public List<UserLab> GetAllUsers();
        public UserLabLoginResponse VerifyLogin(UserLabVerificationInput vLoginInput);
    }
}
