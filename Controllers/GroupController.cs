using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.Controllers
{
    [Authorize]
    [Route("api/grouplist")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly ILogger<GroupController> _logger; 


        public GroupController(IGroupService groupService, ILogger<GroupController> logger)
        {
            _groupService = groupService;
            _logger = logger; 

        }

        [HttpPost]
        [Route("list")]
        public GroupResponse ListLogList()
        {

            GroupResponse res = new GroupResponse();

            try
            {
                res = _groupService.GetLogList();
                _logger.LogInformation("Controller received data: {Data}", res.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the controller.");
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }
    }
}
