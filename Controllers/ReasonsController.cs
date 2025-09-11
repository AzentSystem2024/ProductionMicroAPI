using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReasonsController : ControllerBase
    {
        private readonly IReasonsService _reasonsService;
        public ReasonsController(IReasonsService reasonsService)
        {
            _reasonsService = reasonsService;
        }
        [HttpPost]
        public List<Reasons> List()
        {
            List<Reasons> Products = new List<Reasons>();

            try
            {
                
                Products = _reasonsService.GetAllReasons();
            }
            catch (Exception ex)
            {
            }
            return Products.ToList();
        }

        [HttpPost]
        [Route("select/{id:int}")]
        public Reasons Select(int id)
        {

            Reasons objReasons = new Reasons();
            try
            {
                
                objReasons = _reasonsService.GetItems(id);
            }
            catch (Exception ex)
            {

            }

            return objReasons;
        }

        [HttpPost]
        [Route("insert")]
        public Response Insert(Reasons reasonsData)
        {
            Response res = new Response();

            try
            {
                
                _reasonsService.Insert(reasonsData);

                res.flag = "1";
                res.message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = "0"; // Set flag to indicate failure
                res.message = ex.Message;
            }

            return res;
        }

        [HttpPost]
        [Route("update")]
        public Response Update(Reasons reasonsData)
        {
            Response res = new Response();

            try
            {
                
                _reasonsService.Update(reasonsData);
                res.flag = "1";
                res.message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }

            return res;
        }

        [HttpGet]
        [Route("delete/{id:int}")]
        public Response Delete(int id)
        {
            Response res = new Response();

            try
            {
                

                _reasonsService.DeleteReasons(id);
                res.flag = "1";
                res.message = "Success";
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
