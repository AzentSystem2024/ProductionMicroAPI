using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ILoginService
    {
        LoginResponse VerifyLogin(Login loginInput);

    }
}
