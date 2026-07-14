using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SynchController : ControllerBase
    {
        private readonly ISynchService _synchService;
        public SynchController(ISynchService synchService)
        {
            _synchService = synchService;
        }
        [HttpPost]
        [Route("Upload")]
        public SynchResponse UploadData(Synch model)
        {
            SynchResponse response = new SynchResponse();

            try
            {
                response = _synchService.UploadData(model);

                
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }
        [HttpGet]
        [Route("test")]
        public IActionResult Get()
        {
            Test response = new Test();

            try
            {
                using (SqlConnection conn = ADO.GetConnection())
                {
                    response.Flag = 1;
                    response.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
            }

            return Ok(response);
        }
        [HttpPost]
        [Route("download")]
        public SynchDownloadResponse DownloadData(SynchDownload model)
        {
            SynchDownloadResponse response = new SynchDownloadResponse();

            try
            {
                response = _synchService.DownloadData(model);

            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }
        [HttpPost("PendingStores")]
        public IActionResult GetPendingStores()
        {
            try
            {
                var result = _synchService.GetSynchPendingStores();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("UpdateLastSynchTime")]
        public IActionResult UpdateLastSynchTime(UpdateLastSynchTimeRequest request)
        {
            try
            {
                var response = _synchService.UpdateLastSynchTime(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
