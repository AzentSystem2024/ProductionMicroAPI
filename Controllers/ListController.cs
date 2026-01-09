using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using MicroApi.Models;

namespace MicroApi.Controllers
{
    [Route("api/listGroupHead")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly IListGroupHeadService _listGroupHeadService;

        public ListController(IListGroupHeadService listGroupHeadService)
        {
            _listGroupHeadService = listGroupHeadService;
        }

        [HttpPost]
        [Route("list")]
        public ListResponse ListLogList()
        {

            ListResponse res = new ListResponse();

            try
            {
                res = _listGroupHeadService.GetLogList();
                //var response = _listGroupHeadService.GetLogList();
                //return Ok(response);
            }
            //catch (System.Exception ex)
            //{
            //    return StatusCode(500, new ListResponse { flag = 0, Message = ex.Message });
            //}
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }
    }
}

