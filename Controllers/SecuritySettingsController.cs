using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecuritySettingsController : ControllerBase
    {
        private readonly ISecuritySettingsService _securitySettingsService;
        public SecuritySettingsController(ISecuritySettingsService securitySettingsService)
        {
            _securitySettingsService = securitySettingsService;
        }
        [HttpPost("list")]
        public SecuritySettingResponse List()
        {
            SecuritySettingResponse res = new SecuritySettingResponse();
            try
            {
                int userID = 1;
                res = _securitySettingsService.GetAllSecuritySetting(userID);

                if (res != null)
                {
                    res.flag = "1";
                    res.message = "Success";
                }
                else
                {
                    res.flag = "0";
                    res.message = "No data found.";
                }
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }
            return res;
        }

        [HttpPost("save")]
        public SecuritySettingResponse Save(SecuritySettings objSecurity)
        {
            SecuritySettingResponse res = new SecuritySettingResponse();
            try
            {
                int userID = 1;
                _securitySettingsService.Save(objSecurity, userID);
                res.flag = "1";
                res.message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }
            return res;
        }
    }
}
