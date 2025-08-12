using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.DataLayer.Services;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepreciationController : ControllerBase
    {
        private readonly IDepreciationService _depreciationService;

        public DepreciationController (IDepreciationService depreciationService)
        {
            _depreciationService = depreciationService;
        }
        [HttpPost]
        [Route("FixedAsset/list")]
        public DepreciationResponse FixedAssetList()
        {
            DepreciationResponse response = new DepreciationResponse();
            try
            {
                response = _depreciationService.GetFixedAssetsList();
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
                response.Data = new List<FixedAssetLists>();
            }
            return response;
        }
        [HttpPost]
        [Route("DepreciationList/List")]
        public DepreciationListResponse DepreciationList()
        {
            DepreciationListResponse res = new DepreciationListResponse();
            try
            {
                res = _depreciationService.GetList();
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = ex.Message;
                res.Data = new List<DepreciationList>();
            }
            return res;
        }
        [HttpPost("Insert")]
        public IActionResult InsertDepreciation([FromBody] DepreciationInsertRequest request)
        {
            try
            {
                var result = _depreciationService.InsertDepreciation(request);
                return Ok(new { Flag = 1, Message = "Success", Data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Flag = 0, Message = $"Error inserting data: {ex.Message}" });
            }
        }

        [HttpPost("update")]
        public IActionResult UpdateDepreciation([FromBody] DepreciationUpdateRequest request)
        {
            try
            {
                var result = _depreciationService.UpdateDepreciation(request);
                return Ok(new { Flag = 1, Message = "Success", Data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Flag = 0, Message = $"Error updating data: {ex.Message}" });
            }
        }
        [HttpPost("approve")]
        public IActionResult ApproveDepreciation([FromBody] DepreciationApproveRequest request)
        {
            try
            {
                var result = _depreciationService.ApproveDepreciation(request);
                return Ok(new { Flag = 1, Message = "Success", Data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Flag = 0, Message = $"Error approving data: {ex.Message}" });
            }
        }
        [HttpPost("select/{id}")]
        public IActionResult GetDepreciationById(int id)
        {
            try
            {
                var response = _depreciationService.GetDepreciationById(id);
                if (response.Flag == 1)
                {
                    return Ok(response);
                }
                return NotFound(response.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("delete/{id}")]
        public IActionResult DeleteDepreciation(int id)
        {
            try
            {
                var result = _depreciationService.DeleteDepreciation(id);
                return Ok(new { Flag = 1, Message = "Success", Data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Flag = 0, Message = $"Error deleting data: {ex.Message}" });
            }
        }
    }
}

