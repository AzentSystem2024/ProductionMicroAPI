using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserSecurityController : ControllerBase
    {
        private IUserSecurityService _Service;
        public UserSecurityController(IUserSecurityService Service)
        {
            _Service = Service;
        }

        [HttpPost]
        [Route("usersecuritylist")]
        public UserSecurityResponse List()
        {
            UserSecurityResponse res = new UserSecurityResponse();

            try
            {
                string apiKey = "";
                Int32 intUserID = 1;

                res = _Service.GetAllUserSecurity(intUserID);

                if (res != null)
                {
                    res.flag = 1;
                    res.message = "Success";
                }
                else
                {
                    res.flag = 0;
                    res.message = "No data found.";
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.message = ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("password")]
        public ChangePasswordResponse Insert(ChangePassword objChangePassword)
        {
            ChangePasswordResponse res = new ChangePasswordResponse();
            Int32 intUserID = 1;

            try
            {
                res = _Service.UpdateUserPassword(objChangePassword, intUserID);
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.Message = ex.Message;
            }

            return res;
        }

    }
}

