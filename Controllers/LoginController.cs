using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        [Route("login")]
        public LoginResponse VerifyLogin(Login vLoginInput)
        {
            LoginResponse res = new LoginResponse();

            try
            {
                res = _loginService.VerifyLogin(vLoginInput);
            }

            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;

        }
        [HttpPost]
        [Route("initdata")]
        public InitLoginResponse InitLoginData(Login request)
        {
            InitLoginResponse res = new InitLoginResponse();

            try
            {
                res = _loginService.InitLoginData(request.LOGIN_NAME);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }

        [HttpPost]
        [Route("logout")]
        public LogOutResponse Logout(LogOutRequest request)
        {
            LogOutResponse res = new LogOutResponse();

            try
            {
                res = _loginService.Logout(request);
            }

            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;

        }
    }
}
