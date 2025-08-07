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
    }
}
