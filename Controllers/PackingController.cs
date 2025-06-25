using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
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
        public IActionResult GetSizesForCombination([FromQuery] string artNo, [FromQuery] string color, [FromQuery] int categoryID, [FromQuery] int unitID)
        {
            var sizes = _packingService.GetArticleSizesForCombination(artNo, color, categoryID, unitID);
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
        public PackingResponse Select(int id)
        {
            PackingResponse res = new PackingResponse();
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
        public PackingListResponses GetPackingList()
        {
            PackingListResponses res = new PackingListResponses();
            try
            {
                res = _packingService.GetPackingList();
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

        //[HttpPost]
        //[Route("LastOrderNo/{unitId:int}")]
        //public IActionResult GetLastOrderNo(int unitId)
        //{
        //    try
        //    {
        //        var lastOrderNo = _articleService.GetLastOrderNoByUnitId(unitId);
        //        return Ok(new { LastOrderNo = lastOrderNo });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { flag = 0, message = ex.Message });
        //    }
        //}
    }
}
