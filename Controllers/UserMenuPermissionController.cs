using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserMenuPermissionController : ControllerBase
    {
        private readonly IUserMenuPermissionService _userMenuPermissionService;

        public UserMenuPermissionController(IUserMenuPermissionService userMenuPermissionService)
        {
            _userMenuPermissionService = userMenuPermissionService;
        }

        [HttpPost]
        public IActionResult CheckUserMenuPermissions([FromBody] UserMenuPermissionRequest request)
        {
            try
            {
                var permissions = _userMenuPermissionService.GetUserMenuPermissions(request.UserId, request.MenuId);
                return Ok(permissions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
