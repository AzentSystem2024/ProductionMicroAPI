using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LandedCostController : ControllerBase
    {
        private readonly ILandedCostService _landedCostService;
        public LandedCostController(ILandedCostService landedCostService)
        {
            _landedCostService = landedCostService;
        }
        [HttpPost]
        [Route("list")]
        public List<LandedCost> List()
        {
            List<LandedCost> landedCosts = new List<LandedCost>();

            try
            {
                
                landedCosts = _landedCostService.GetAllLandedCost();
            }
            catch (Exception ex)
            {
            }
            return landedCosts.ToList();
        }

        [HttpPost]
        [Route("select/{id:int}")]
        public LandedCost Select(int id)
        {
            LandedCost objLandedCost = new LandedCost();
            try
            {
                
                objLandedCost = _landedCostService.GetItems(id);
            }
            catch (Exception ex)
            {

            }

            return objLandedCost;
        }

        [HttpPost]
        [Route("save")]
        public LandedCostResponse SaveData(LandedCost LandedCostData)
        {
            LandedCostResponse res = new LandedCostResponse();

            try
            {
                
                Int32 ID = _landedCostService.SaveData(LandedCostData);

                res.flag = "1";
                res.message = "Success";
                res.data = _landedCostService.GetItems(ID);
            }
            catch (Exception ex)
            {
                res.flag = "1";
                res.message = ex.Message;
            }

            return res;
        }

        [HttpPost]
        [Route("delete/{id:int}")]
        public LandedCostResponse Delete(int id)
        {
            LandedCostResponse res = new LandedCostResponse();

            try
            {
                

                _landedCostService.DeleteLandedCost(id);
                res.flag = "1";
                res.message = "Success";
                res.data = _landedCostService.GetItems(id);
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }
            return res;
        }
    }
}
