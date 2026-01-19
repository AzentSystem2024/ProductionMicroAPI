using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoxProductionController : ControllerBase
    {
        private readonly IBoxProductionService _boxProductionService;

        public BoxProductionController(IBoxProductionService boxProductionService)
        {
            _boxProductionService = boxProductionService;
        }
        [HttpPost]
        [Route("insert")]
        public BoxProdResponse Insert(BoxProduction model)
        {
            BoxProdResponse res = new BoxProdResponse();

            try
            {
                res = _boxProductionService.Insert(model);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("packingbomlist")]
        public PackingBOMResponse GetPackingBomList(PackingBOMRequest model)
        {
            PackingBOMResponse res = new PackingBOMResponse();

            try
            {
                res = _boxProductionService.GetPackingBomList(model);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("select/{id:int}")]
        public BoxProductionSelectResponse GetProductionById(int id)
        {
            BoxProductionSelectResponse res = new BoxProductionSelectResponse();

            try
            {
                res = _boxProductionService.GetProductionById(id);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("packinglist")]
        public BoxProductionListResponse articleprodlist(BoxProductionListRequest model)
        {
            BoxProductionListResponse res = new BoxProductionListResponse();

            try
            {
                res = _boxProductionService.packingprodlist(model);
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
