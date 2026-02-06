using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UomController : ControllerBase
    {
        private readonly IUomService _uomService;
        public UomController(IUomService uomService)
        {
            _uomService = uomService;
        }
        [HttpPost]
        [Route("list")]
        public List<Uom> List(UOMListReq request)
        {
            List<Uom> uoms = new List<Uom>();
            UomResponse res = new UomResponse();
            try
            {
                
                uoms = _uomService.GetAllUom(request);

                res.flag = "1";
                res.message = "Success";
            }
            catch (Exception ex)
            {
            }
            return uoms.ToList();
        }

        [HttpPost]
        [Route("select/{id:int}")]
        public Uom Select(int id)
        {
            Uom objUom = new Uom();
            Uom res = new Uom();
            try
            {
                
                objUom = _uomService.GetItems(id);

                res.flag = "1";
                res.message = "Success";

            }
            catch (Exception ex)
            {

            }

            return objUom;
        }

        [HttpPost]
        [Route("insert")]
        public UomResponse insert(Uom uom)
        {
            UomResponse res = new UomResponse();
            try
            {
                
                _uomService.Insert(uom);

                res.flag = "1";
                res.message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = "1";
                res.message = ex.Message;
            }

            return res;
        }

        [HttpPost]
        [Route("update")]
        public UomResponse Update(Uom uom)
        {
            UomResponse res = new UomResponse();
            try
            {
                
                _uomService.Update(uom);

                res.flag = "1";
                res.message = "Success";
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
        public Uom Delete(int id)
        {
            Uom res = new Uom();

            try
            {
                

                _uomService.DeleteUom(id);
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
