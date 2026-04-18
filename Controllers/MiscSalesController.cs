using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MiscSalesController : ControllerBase
    {
        private readonly IMiscSalesService _miscSalesService;

        public MiscSalesController(IMiscSalesService miscSalesService)
        {
            _miscSalesService = miscSalesService;
        }
        [HttpPost]
        [Route("insert")]
        public MiscSalesResponse Insert(MiscSales input)
        {
            MiscSalesResponse res = new MiscSalesResponse();

            try
            {
                res = _miscSalesService.Insert(input);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("list")]
        public MiscSalesResponse GetMiscSalesList(MiscSalesListRequest input)
        {
            MiscSalesResponse res = new MiscSalesResponse();

            try
            {
                res = _miscSalesService.GetMiscSalesList(input);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;

                res.List = new List<MiscSaleList>(); 
                res.Data = null;                     
            }

            return res;
        }
        [HttpPost]
        [Route("Update")]
        public MiscSalesResponse Update(MiscSales input)
        {
            MiscSalesResponse res = new MiscSalesResponse();

            try
            {
                res = _miscSalesService.Update(input);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("Commit")]
        public MiscSalesResponse Commit(MiscSales input)
        {
            MiscSalesResponse res = new MiscSalesResponse();

            try
            {
                res = _miscSalesService.Commit(input);
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
        public MiscSalesViewResponse GetMiscSalesById(int Id)
        {
            MiscSalesViewResponse response = new MiscSalesViewResponse();
            try
            {
                response = _miscSalesService.GetMiscSalesById(Id);
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = "Error: " + ex.Message;
            }
            return response;
        }
        [HttpPost]
        [Route("delete/{id:int}")]
        public MiscSalesResponse Delete(int id)
        {
            MiscSalesResponse res = new MiscSalesResponse();
            try
            {
                res = _miscSalesService.Delete(id);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
    }
}
