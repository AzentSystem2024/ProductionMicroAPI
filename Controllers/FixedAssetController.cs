using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FixedAssetController : ControllerBase
    {
        private readonly IFixedAssetService _fixedAssetService;
        public FixedAssetController(IFixedAssetService fixedAssetService)
        {
            _fixedAssetService = fixedAssetService;
        }

        [HttpPost]
        [Route("list")]
        public FixedAssetResponse FixedAssetList()
        {
            FixedAssetResponse res = new FixedAssetResponse();
            try
            {
                res = _fixedAssetService.GetFixedAssetList();
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
                res.Data = new List<FixedAssetList>();
            }

            return res;

        }
    }
}

