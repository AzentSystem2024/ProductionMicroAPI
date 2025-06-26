using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserRoleDropdownController : ControllerBase
    {
        private readonly IUserRoleDropdownService _userRoleDropdownService;

        public UserRoleDropdownController(IUserRoleDropdownService userRoleDropdownService)
        {
            _userRoleDropdownService = userRoleDropdownService;
        }

        [HttpPost]
        public List<UserRoleDropdown> ListData()
        {
            List<UserRoleDropdown> data = new List<UserRoleDropdown>();
            try
            {
                data = _userRoleDropdownService.GetDropDownData();
               
            }
            catch (Exception ex)
            {
               

            }
            return data;
        }

    }
}
