using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        [Route("insert")]
        public UserResponse Insert(User user)
        {
            UserResponse res = new UserResponse();
            try
            {
                res = _userService.Insert(user);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
        [HttpPost]
        [Route("update")]

        public UserResponse Update(UserUpdate user)
        {
            UserResponse res = new UserResponse();
            try
            {
                res = _userService.Update(user);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
        [HttpPost]
        [Route("select/{id:int}")]
        public UserSelectResponse select(int id)
        {
            UserSelectResponse res = new UserSelectResponse();

            try
            {
                res = _userService.GetUserById(id);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("list")]
        public UserListResponse DeptLogList()
        {

            UserListResponse res = new UserListResponse();
            try
            {
                res = _userService.GetUserList();

            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }

        [HttpPost]
        [Route("delete/{id:int}")]
        public UserResponse Delete(int id)
        {
            UserResponse res = new UserResponse();
            try
            {
                res = _userService.DeleteUserData(id);
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
        public IActionResult LogoutUser([FromBody] UserLogout logout)
        {
            try
            {
                int rowsAffected = _userService.Logout(logout);

                if (rowsAffected > 0)
                {
                    return Ok(new { Success = true, Message = "Logout successful" });
                }
                else
                {
                    return BadRequest("Invalid token or logout failed");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal Server Error", Error = ex.Message });
            }
        }
    }
}
