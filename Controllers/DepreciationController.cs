using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost]
        [Route("Insert")]
        public IActionResult InsertDepreciation([FromBody] DepreciationInsertRequest request)
        {
            var response = _depreciationService.InsertDepreciation(request);
            if (response.Flag == 1)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}

