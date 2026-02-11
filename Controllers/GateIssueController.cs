using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GateIssueController : ControllerBase
    {
        private readonly IGateIssueService _gateIssueService;

        public GateIssueController(IGateIssueService gateIssueService)
        {
            _gateIssueService = gateIssueService;
        }
        [HttpPost]
        [Route("insert")]
        public GateIssueResponse InsertGateIssue(GateIssue model)
        {
            GateIssueResponse res = new GateIssueResponse();

            try
            {
                res = _gateIssueService.InsertGateIssue(model);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
    }
}
