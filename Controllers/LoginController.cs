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
    }
}
