using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TendersController : ControllerBase
    {
        private readonly ITendersService _tendersService;
        public TendersController(ITendersService tendersService)
        {
            _tendersService = tendersService;
        }
        [HttpPost]
        [Route("list")]
        public List<Tenders> List()
        {
            List<Tenders> tenders = new List<Tenders>();

            try
            {
                
                tenders = _tendersService.GetAllTender();
            }
            catch (Exception ex)
            {
            }
            return tenders.ToList();
        }

        [HttpPost]
        [Route("select/{id:int}")]
        public Tenders Select(int id)
        {
            Tenders objTenders = new Tenders();
            try
            {
                
                objTenders = _tendersService.GetItems(id);
            }
            catch (Exception ex)
            {

            }

            return objTenders;
        }

        [HttpPost]
        [Route("save")]
        public TendersResponse SaveData(Tenders tendersData)
        {
            TendersResponse res = new TendersResponse();

            try
            {
                
                Int32 ID = _tendersService.SaveData(tendersData);

                res.flag = "1";
                res.message = "Success";
                res.data = _tendersService.GetItems(ID);
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
        public TendersResponse Delete(int id)
        {
            TendersResponse res = new TendersResponse();

            try
            {
                

                _tendersService.DeleteTenders(id);
                res.flag = "1";
                res.message = "Success";
                res.data = _tendersService.GetItems(id);
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
