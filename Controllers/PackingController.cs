using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using MicroApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    //[Authorize]
    [Route("api/packing")]
    [ApiController]
    public class PackingController : ControllerBase
    {
        private readonly IPackingService _packingService;

        public PackingController(IPackingService packingService)
        {
            _packingService = packingService;
        }
        [HttpPost("production-units")]
        public IActionResult GetProductionUnits()
        {
            var units = _packingService.GetProductionUnits();
            return Ok(units);
        }

        [HttpPost("sizes-for-combination")]
        public IActionResult GetSizesForCombination(ArticleSizeCombinationRequest request)
        {
            var sizes = _packingService.GetArticleSizesForCombination(request);
            return Ok(sizes);
        }


        [HttpPost("suppliers")]
        public IActionResult GetSuppliers()
        {
            var suppliers = _packingService.GetSuppliers();
            return Ok(suppliers);
        }

        [HttpPost]
        [Route("Insert")]
        public PackingResponse Insert([FromBody] PackingMasters packing)
        {
            PackingResponse res = new PackingResponse();
            try
            {
                res = _packingService.Insert(packing);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }

        [HttpPost]
        [Route("Update")]
        public PackingResponse Update([FromBody] PackingUpdate packing)
        {
            PackingResponse res = new PackingResponse();
            try
            {
                res = _packingService.Update(packing);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }

        [HttpPost]
        [Route("Select/{id:int}")]
        public PackingSelectResponse Select(int id)
        {
            PackingSelectResponse res = new PackingSelectResponse();
            try
            {
                res = _packingService.GetPackingById(id);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }

        [HttpPost]
        [Route("List")]
        public PackingListResponses GetPackingList(PackingListReq request)
        {
            PackingListResponses res = new PackingListResponses();
            try
            {
                res = _packingService.GetPackingList(request);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
        [HttpPost]
        [Route("Delete/{id:int}")]
        public PackingResponse Delete(int id)
        {
            PackingResponse res = new PackingResponse();
            try
            {
                res = _packingService.DeletePackingData(id);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }

        [HttpPost]
        [Route("Lastaliasno")]
        public IActionResult GetAliasNo()
        {
            try
            {
                var GetAliasNo = _packingService.GetAliasNo();
                return Ok(new { GetAliasNo = GetAliasNo });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { flag = 0, message = ex.Message });
            }
        }
        [HttpPost]
        [Route("InsertPackingPriceLog")]
        public PackingResponse ChangeStandardPrice(ChangeStandardPriceModel model)
        {
            PackingResponse res = new PackingResponse();
            try
            {
                res = _packingService.ChangeStandardPrice(model);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
        [HttpPost]
        [Route("GetPackingPriceLog")]
        public PackingPriceLogResponse GetPackingPriceLog(PackingPriceLogrequest request)
        {
            PackingPriceLogResponse res = new PackingPriceLogResponse();

            try
            {
                res.Data = _packingService.GetPackingPriceLog(request);
                res.flag = 1;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }

    }
}
